using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sunshine.App.Data;
using Sunshine.App.DTOs;
using Sunshine.App.Models;

namespace Sunshine.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ApplicationDbContext _db;
    private readonly IConfiguration _config;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ApplicationDbContext db,
        IConfiguration config)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _db = db;
        _config = config;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
    {
        var email = request.Email.Trim().ToLowerInvariant();
        var user = await _userManager.Users
            .Include(u => u.PatientProfile)
            .Include(u => u.DoctorProfile)
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return Unauthorized(new AuthResponse(false, "Invalid email or password.", null, null));
        }

        if (!user.IsActive)
        {
            return Unauthorized(new AuthResponse(false, "Your account has been deactivated. Please contact support.", null, null));
        }

        var token = GenerateJwtToken(user);
        return Ok(new AuthResponse(true, "Login successful.", token, MapToUserDto(user)));
    }

    [HttpPost("register/patient")]
    public async Task<ActionResult<AuthResponse>> RegisterPatient([FromBody] RegisterPatientRequest request)
    {
        var email = request.Email.Trim().ToLowerInvariant();
        if (await _userManager.FindByEmailAsync(email) != null)
        {
            return BadRequest(new AuthResponse(false, "An account with this email already exists.", null, null));
        }

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FullName = request.FullName.Trim(),
            PhoneNumber = request.Phone?.Trim(),
            Role = AppRoles.Patient,
            IsActive = true,
            EmailConfirmed = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return BadRequest(new AuthResponse(false, string.Join(" ", result.Errors.Select(e => e.Description)), null, null));
        }

        await _userManager.AddToRoleAsync(user, AppRoles.Patient);

        var patient = new Patient
        {
            UserId = user.Id,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender?.Trim(),
            EmergencyContact = request.EmergencyContact?.Trim(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _db.Patients.Add(patient);
        await _db.SaveChangesAsync();

        var token = GenerateJwtToken(user);
        user.PatientProfile = patient;
        return Ok(new AuthResponse(true, "Patient account created successfully.", token, MapToUserDto(user)));
    }

    [HttpPost("register/doctor")]
    public async Task<ActionResult<AuthResponse>> RegisterDoctor([FromBody] RegisterDoctorRequest request)
    {
        var email = request.Email.Trim().ToLowerInvariant();
        if (await _userManager.FindByEmailAsync(email) != null)
        {
            return BadRequest(new AuthResponse(false, "An account with this email already exists.", null, null));
        }

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FullName = request.FullName.Trim(),
            PhoneNumber = request.Phone?.Trim(),
            Role = AppRoles.Doctor,
            IsActive = true,
            EmailConfirmed = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return BadRequest(new AuthResponse(false, string.Join(" ", result.Errors.Select(e => e.Description)), null, null));
        }

        await _userManager.AddToRoleAsync(user, AppRoles.Doctor);

        var doctor = new Doctor
        {
            UserId = user.Id,
            Specialization = request.Specialization.Trim(),
            ConsultationFee = request.ConsultationFee > 0 ? request.ConsultationFee : 100.00m,
            PaymentPolicy = request.PaymentPolicy?.ToUpperInvariant() == PaymentPolicies.PostPayment ? PaymentPolicies.PostPayment : PaymentPolicies.Advance,
            Bio = request.Bio?.Trim(),
            ExperienceYears = request.ExperienceYears,
            Qualification = request.Qualification?.Trim(),
            IsAvailable = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _db.Doctors.Add(doctor);
        await _db.SaveChangesAsync();

        // Default working schedules (Mon-Fri)
        for (int day = 1; day <= 5; day++)
        {
            _db.DoctorSchedules.Add(new DoctorSchedule
            {
                DoctorId = doctor.Id,
                DayOfWeek = day,
                StartTime = new TimeOnly(9, 0),
                EndTime = new TimeOnly(17, 0),
                SlotDurationMinutes = 45,
                IsActive = true
            });
        }
        await _db.SaveChangesAsync();

        var token = GenerateJwtToken(user);
        user.DoctorProfile = doctor;
        return Ok(new AuthResponse(true, "Doctor account created successfully.", token, MapToUserDto(user)));
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetMe()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var user = await _userManager.Users
            .Include(u => u.PatientProfile)
            .Include(u => u.DoctorProfile)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null) return NotFound();
        return Ok(MapToUserDto(user));
    }

    private string GenerateJwtToken(ApplicationUser user)
    {
        var jwtKey = _config["Jwt:Key"] ?? "SunshineCounselingSecretKey2026!MustBeVerySecureAnd32BytesLong";
        var jwtIssuer = _config["Jwt:Issuer"] ?? "Sunshine.Api";
        var jwtAudience = _config["Jwt:Audience"] ?? "Sunshine.Client";

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email ?? ""),
            new(ClaimTypes.Name, user.FullName),
            new(ClaimTypes.Role, user.Role)
        };

        if (user.PatientProfile != null)
        {
            claims.Add(new Claim("PatientId", user.PatientProfile.Id.ToString()));
        }
        if (user.DoctorProfile != null)
        {
            claims.Add(new Claim("DoctorId", user.DoctorProfile.Id.ToString()));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddDays(7);

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static UserDto MapToUserDto(ApplicationUser user)
    {
        return new UserDto(
            user.Id,
            user.Email ?? "",
            user.FullName,
            user.PhoneNumber,
            user.Role,
            user.IsActive,
            user.PatientProfile?.Id,
            user.DoctorProfile?.Id,
            user.CreatedAt
        );
    }
}

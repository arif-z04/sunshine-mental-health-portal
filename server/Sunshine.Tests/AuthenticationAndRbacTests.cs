using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Sunshine.App.DTOs;
using Sunshine.App.Models;
using Xunit;

namespace Sunshine.Tests;

public class AuthenticationAndRbacTests
{
    private const string JwtSecret = "SunshineCounselingSecretKey2026!MustBeVerySecureAnd32BytesLong";
    private const string Issuer = "Sunshine.Api";
    private const string Audience = "Sunshine.Client";

    private string GenerateTestJwt(int userId, string email, string role)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(ClaimTypes.Email, email),
            new(ClaimTypes.Role, role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [Theory]
    [InlineData(AppRoles.Admin)]
    [InlineData(AppRoles.Doctor)]
    [InlineData(AppRoles.Patient)]
    public void JwtTokenGeneration_GeneratesValidTokenWithExpectedClaims(string role)
    {
        var token = GenerateTestJwt(101, "user@example.com", role);
        Assert.NotNull(token);

        var handler = new JwtSecurityTokenHandler();
        var validationParams = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Issuer,
            ValidAudience = Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecret))
        };

        var principal = handler.ValidateToken(token, validationParams, out var validatedToken);
        Assert.NotNull(validatedToken);

        var roleClaim = principal.FindFirst(ClaimTypes.Role)?.Value;
        Assert.Equal(role, roleClaim);

        var idClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Assert.Equal("101", idClaim);
    }

    [Fact]
    public void NonAdminRole_CannotAssumeAdminRole()
    {
        var patientToken = GenerateTestJwt(102, "patient@example.com", AppRoles.Patient);
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(patientToken);

        var roleClaim = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role || c.Type == "role")?.Value;
        Assert.NotEqual(AppRoles.Admin, roleClaim);
        Assert.Equal(AppRoles.Patient, roleClaim);
    }
}

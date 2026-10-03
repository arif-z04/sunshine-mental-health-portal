using System;
using System.Text.RegularExpressions;
using Xunit;

namespace Sunshine.Tests;

public class BangladeshRequirementsTests
{
    private static readonly Regex BdPhoneRegex = new(@"^(?:\+8801|01)[3-9]\d{8}$", RegexOptions.Compiled);

    [Theory]
    [InlineData("+8801711223344", true)]
    [InlineData("+8801819000102", true)]
    [InlineData("+8801914000103", true)]
    [InlineData("01711000001", true)]
    [InlineData("01312345678", true)]
    [InlineData("+14155552671", false)] // US phone number
    [InlineData("12345", false)]
    [InlineData("01234567890", false)] // Invalid operator prefix '2'
    public void BangladeshPhoneNumberValidation_ValidatesCorrectly(string phoneNumber, bool expectedValid)
    {
        var isValid = BdPhoneRegex.IsMatch(phoneNumber.Replace(" ", "").Replace("-", ""));
        Assert.Equal(expectedValid, isValid);
    }

    [Fact]
    public void AsiaDhakaTimeZone_IsAvailableAndUtcPlus6()
    {
        // Asia/Dhaka is UTC+6 standard time
        var tz = TimeZoneInfo.FindSystemTimeZoneById("Asia/Dhaka");
        Assert.NotNull(tz);
        Assert.Equal(TimeSpan.FromHours(6), tz.BaseUtcOffset);
    }

    [Theory]
    [InlineData(1500, "৳1,500")]
    [InlineData(499, "৳499")]
    [InlineData(3999, "৳3,999")]
    public void BdtCurrencyFormatting_FormatsWithTakaSign(decimal amount, string expectedFormatted)
    {
        var formatted = $"৳{amount:N0}";
        Assert.Equal(expectedFormatted, formatted);
    }
}

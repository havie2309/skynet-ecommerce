using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Skinet.Api.DTOs;

namespace Skinet.Api.Tests;

public class AuthDtoValidationTests
{
    [Fact]
    public void RegisterDto_Should_Be_Invalid_When_Email_Is_Missing()
    {
        var dto = new RegisterDto("", "Test1234");

        var results = ValidateModel(dto);

        results.Should().Contain(r => r.MemberNames.Contains("Email"));
    }

    [Fact]
    public void RegisterDto_Should_Be_Invalid_When_Password_Is_Weak()
    {
        var dto = new RegisterDto("test@example.com", "weak");

        var results = ValidateModel(dto);

        results.Should().Contain(r => r.MemberNames.Contains("Password"));
    }

    [Fact]
    public void RegisterDto_Should_Be_Valid_When_Email_And_Password_Are_Valid()
    {
        var dto = new RegisterDto("test@example.com", "Test1234");

        var results = ValidateModel(dto);

        results.Should().BeEmpty();
    }

    [Fact]
    public void LoginDto_Should_Be_Invalid_When_Email_Is_Invalid()
    {
        var dto = new LoginDto("not-an-email", "Test1234");

        var results = ValidateModel(dto);

        results.Should().Contain(r => r.MemberNames.Contains("Email"));
    }

    private static List<ValidationResult> ValidateModel(object model)
    {
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(model);

        Validator.TryValidateObject(model, context, validationResults, validateAllProperties: true);

        return validationResults;
    }
}

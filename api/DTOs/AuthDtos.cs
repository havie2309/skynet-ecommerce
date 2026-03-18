using System.ComponentModel.DataAnnotations;

namespace Skinet.Api.DTOs;

public record RegisterDto(
    [Required]
    [EmailAddress]
    string Email,

    [Required]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    [RegularExpression(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one number.")]
    string Password);

public record LoginDto(
    [Required]
    [EmailAddress]
    string Email,

    [Required]
    string Password);

public record AuthResponseDto(string Token, string Email, string Role);
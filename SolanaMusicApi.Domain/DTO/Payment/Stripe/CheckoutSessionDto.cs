using System.ComponentModel.DataAnnotations;

namespace SolanaMusicApi.Domain.DTO.Payment.Stripe;

public class CheckoutSessionDto
{
    [Required]
    public string ProductName { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    [Required]
    public decimal Amount { get; set; }
    [Required]
    public string ImageUrl { get; set; } = string.Empty;
    [Required]
    public string Currency { get; set; } = string.Empty;
    [Required]
    public string SuccessUrl { get; set; } = string.Empty;
    [Required]
    public string CancelUrl { get; set; } = string.Empty;
}

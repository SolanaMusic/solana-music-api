using System.ComponentModel.DataAnnotations;

namespace SolanaMusicApi.Domain.DTO.Whitelist;

public class WhitelistRequestDto : IValidatableObject
{
    public string? Name { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string? WalletAddress { get; set; }
    public bool IsArtist { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var hasNameAndEmail = !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Email);
        var hasWallet = !string.IsNullOrWhiteSpace(WalletAddress);

        if (!hasWallet && !hasNameAndEmail)
            yield return new ValidationResult("Either WalletAddress or both Name and Email must be provided");
    }
}
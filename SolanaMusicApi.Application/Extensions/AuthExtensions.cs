using System.Text;
using Org.BouncyCastle.Security;
using SolanaMusicApi.Domain.DTO.Auth.OAuth;
using Solnet.Wallet;

namespace SolanaMusicApi.Application.Extensions;

public static class AuthExtensions
{
    public static void VerifySignature(SolanaWalletLoginDto solanaWalletLoginDto)
    {
        var signatureBytes = Convert.FromBase64String(solanaWalletLoginDto.Signature);
        var messageBytes = Encoding.UTF8.GetBytes(solanaWalletLoginDto.Message);
        var pubKey = new PublicKey(solanaWalletLoginDto.PublicKey);
        
        var verify = pubKey.Verify(messageBytes, signatureBytes);
        if (!verify)
            throw new SignatureException("Invalid signature");
    }

    public static string GenerateEmail(string snippet, string? provider = null) => 
        $"{provider}{snippet}@solana.net";
    
    public static string CapitalizeFirst(this string input)
    {
        if (string.IsNullOrEmpty(input)) 
            return input;
        
        return char.ToUpper(input[0]) + input[1..].ToLower();
    }
    
    public static string ToWalletProviderName(this string provider) => $"{provider.CapitalizeFirst()} Wallet";
}
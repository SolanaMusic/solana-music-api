using System.Text;
using Org.BouncyCastle.Security;
using SolanaMusicApi.Domain.DTO.Auth.OAuth;
using Solnet.Wallet;

namespace SolanaMusicApi.Application.Extensions;

public static class AuthExtensions
{
    public static void VerifySignature(PhantomLoginDto phantomLoginDto)
    {
        var signatureBytes = Convert.FromBase64String(phantomLoginDto.Signature);
        var messageBytes = Encoding.UTF8.GetBytes(phantomLoginDto.Message);
        var pubKey = new PublicKey(phantomLoginDto.PublicKey);
        
        var verify = pubKey.Verify(messageBytes, signatureBytes);
        if (!verify)
            throw new SignatureException("Invalid signature");
    }

    public static string GenerateEmail(string snippet, string? provider = null) => 
        $"{provider}{snippet}@solana.net";
}
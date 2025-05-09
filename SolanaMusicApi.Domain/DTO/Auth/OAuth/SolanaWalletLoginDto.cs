﻿using System.ComponentModel.DataAnnotations;

namespace SolanaMusicApi.Domain.DTO.Auth.OAuth;

public class SolanaWalletLoginDto
{
    [Required]
    public string PublicKey { get; set; } = string.Empty;
    
    [Required]
    public string Signature { get; set; } = string.Empty;
    
    [Required]
    public string Message { get; set; } = string.Empty;
    
    [Required]
    public string WalletName { get; set; } = string.Empty;
}
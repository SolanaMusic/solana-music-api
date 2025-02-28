using SolanaMusicApi.Domain.Entities;

namespace SolanaMusicApi.Domain.DTO.General.CountryDto;

public class CountryResponseDto : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
}

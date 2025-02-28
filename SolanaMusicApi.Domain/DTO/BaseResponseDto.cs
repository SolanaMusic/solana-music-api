using System.Text.Json.Serialization;

namespace SolanaMusicApi.Domain.DTO;

public class BaseResponseDto
{
    [JsonPropertyOrder(-1)]
    public long Id { get; set; }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace SolanaMusicApi.Domain.Entities;

public class BaseEntity
{
    [Column(Order = 0)]
    public long Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}

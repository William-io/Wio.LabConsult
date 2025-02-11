using System.ComponentModel.DataAnnotations.Schema;
using Wio.LabConsult.Domain.Abstractions;
using Wio.LabConsult.Domain.Categories;
using Wio.LabConsult.Domain.Reviews;
using Wio.LabConsult.Domain.Shared;

namespace Wio.LabConsult.Domain.Consults;

public class Consult : Entity
{
    [Column(TypeName = "NVARCHAR(100)")]
    public string? Name { get; set; }
    [Column(TypeName = "NVARCHAR(4000)")]
    public string? Description { get; set; }
    [Column(TypeName = "DECIMAL(10,2)")]
    public decimal Price { get; set; }
    [Column(TypeName = "NVARCHAR(100)")]
    public string? Employee { get; set; }
    public ConsultStatus Status { get; set; } = ConsultStatus.Active;
    public int Rating { get; set; }
    public int CategoryId { get; set; }
    public int Availability { get; set; }
    public string? Crm { get; set; }
    public Category? Category { get; set; }
    public virtual ICollection<Review>? Reviews { get; set; }
    public virtual ICollection<Image>? Images { get; set; }
}

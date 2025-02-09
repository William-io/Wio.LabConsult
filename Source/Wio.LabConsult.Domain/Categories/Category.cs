using System.ComponentModel.DataAnnotations.Schema;
using Wio.LabConsult.Domain.Abstractions;
using Wio.LabConsult.Domain.Consults;

namespace Wio.LabConsult.Domain.Categories;

public class Category : Entity
{
    [Column(TypeName = "NVARCHAR(100)")]
    public string? Name { get; set; }
    public virtual ICollection<Consult> Consults { get; set; } = null!;
}

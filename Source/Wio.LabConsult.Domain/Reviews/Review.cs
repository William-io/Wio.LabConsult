using System.ComponentModel.DataAnnotations.Schema;
using Wio.LabConsult.Domain.Abstractions;
using Wio.LabConsult.Domain.Consults;

namespace Wio.LabConsult.Domain.Reviews;

public class Review : Entity
{
    [Column(TypeName = "NVARCHAR(100)")] //Fazer mapeamento no settings
    public string? Name { get; set; }
    [Column(TypeName = "NVARCHAR(4000)")]
    public string? Comment { get; set; }
    //public Rating? Rating { get; set; }
    public int Rating { get; set; }
    public int ConsultId { get; set; }
    public virtual Consult? Consult { get; set; }
}

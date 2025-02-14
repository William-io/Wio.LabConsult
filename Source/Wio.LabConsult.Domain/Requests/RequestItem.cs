using System.ComponentModel.DataAnnotations.Schema;
using Wio.LabConsult.Domain.Abstractions;
using Wio.LabConsult.Domain.Consults;

namespace Wio.LabConsult.Domain.Requests;

public class RequestItem : Entity
{
    public Consult? Consult { get; set; }
    public int ConsultId { get; set; }

    [Column(TypeName = "DECIMAL(18, 2)")]
    public decimal Price { get; set; }

    public int Quantity { get; set; }
    public DateTime DateRequest { get; set; }

    public int RequestId { get; set; }
    public Request? Request { get; set; }


    public int ConsultItemId { get; set; }
    public string? ConsultName { get; set; }

    public string? ImageUrl { get; set; }
}

using System.ComponentModel.DataAnnotations.Schema;
using Wio.LabConsult.Domain.Abstractions;
using Wio.LabConsult.Domain.Requests;

namespace Wio.LabConsult.Domain.Orders;

public class Request : Entity
{
    public string? PatientName { get; set; }
    public string? PatientUserName { get; set; }
    public RequestConfirmation? RequestConfirmation { get; set; }
    public IReadOnlyList<RequestItem>? RequestItems { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal SubTotal { get; set; }
    public RequestStatus Status { get; set; } = RequestStatus.Pending;
    [Column(TypeName = "decimal(10,2)")]
    public decimal Total { get; set; }

    public string? PaymentIntentId { get; set; }
    public string? ClientSecret { get; set; }
    public string? StripeApiKey { get; set; }

    //[Column(TypeName = "decimal(10,2)")]
    //public decimal Tax { get; set; }

    //[Column(TypeName = "decimal(10,2)")]
    //public decimal PriceDiscount { get; set; }
}

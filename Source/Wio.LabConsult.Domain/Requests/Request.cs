using System.ComponentModel.DataAnnotations.Schema;
using Wio.LabConsult.Domain.Abstractions;

namespace Wio.LabConsult.Domain.Requests;

public class Request : Entity
{
    public Request() { }
    public Request(
        string? patientName,
        string? patientUserName,
        RequestConfirmation requestConfirmation,
        decimal subTotal,
        decimal total,
        decimal rate,
        decimal priceWithoutPlan)
    {
        PatientName = patientName;
        PatientUserName = patientUserName;
        RequestConfirmation = requestConfirmation;
        SubTotal = subTotal;
        Total = total;
        Rate = rate;
        PriceWithoutPlan = priceWithoutPlan;
    }

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

    //STRIPE
    public string? ClientSecret { get; set; }
    public string? StripeApiKey { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Rate { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal PriceWithoutPlan { get; set; }
}

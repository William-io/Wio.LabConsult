using Wio.LabConsult.Application.Models.Appointment;
using Wio.LabConsult.Domain.Requests;
using Wio.LabConsult.Domain.Requests;

namespace Wio.LabConsult.Application.Features.Requests.VMs;

public class RequestVm
{
    public int Id { get; set; }
    public RequestConfirmation? RequestConfirmation { get; set; }
    public IReadOnlyList<RequestItem>? RequestItems { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Total { get; set; }
    public RequestStatus Status { get; set; }
    public string? PaymentIntentId { get; set; }

    public string? ClientSecret { get; set; }
    public string? StripeApiKey { get; set; }

    public string? PatientName { get; set; }
    public string? PatientUserName { get; set; }


    public int Quantity
    {
        get { return RequestItems!.Sum(x => x.Quantity); }
        set { }
    }

    public string StatusLabel
    {
        get
        {
            switch (Status)
            {
                case RequestStatus.Pending:
                    {
                        return RequestStatusLabel.PENDING;
                    }
                case RequestStatus.Scheduled:
                    {
                        return RequestStatusLabel.SCHEDULED;
                    }
                case RequestStatus.Confirmed:
                    {
                        return RequestStatusLabel.CONFIRMED;
                    }
                case RequestStatus.Canceled:
                    {
                        return RequestStatusLabel.CANCELED;
                    }
                case RequestStatus.Error:
                    {
                        return RequestStatusLabel.ERROR;
                    }
                default: return RequestStatusLabel.ERROR;
            }
        }
    }
}

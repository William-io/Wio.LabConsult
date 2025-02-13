using Wio.LabConsult.Application.Features.Images.Queries.Vms;
using Wio.LabConsult.Application.Features.Reviews.Queries.Vms;
using Wio.LabConsult.Application.Models.Consult;
using Wio.LabConsult.Domain.Consults;

namespace Wio.LabConsult.Application.Features.Consults.Queries.VMs;

public class ConsultVm
{

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }

    public string? Employee { get; set; }
    public ConsultStatus Status { get; set; }
    public int Rating { get; set; }


    public int Availability { get; set; }
    public string? Crm { get; set; }

    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }

    public int NumberReviews { get; set; }

    public virtual ICollection<ReviewVm>? Reviews { get; set; }
    public virtual ICollection<ImageVm>? Images { get; set; }

    public ClinicAddress? Address { get; set; }

    public string StatusLabel
    {
        get
        {
            switch (Status)
            {
                case ConsultStatus.Active:
                    return ConsultStatusLabel.ACTIVE;
                case ConsultStatus.Inactive:
                    return ConsultStatusLabel.INACTIVE;
                default:
                    return ConsultStatusLabel.INACTIVE;
            }
        }
        set { }
    }
}


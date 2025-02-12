using MediatR;
using Wio.LabConsult.Application.Features.Consults.Queries.VMs;

namespace Wio.LabConsult.Application.Features.Consults.Queries.GetConsultById;

public class GetConsultByIdQuery : IRequest<ConsultVm>
{
    public int ConsultId { get; set; }

    public GetConsultByIdQuery(int consultId)
    {
        ConsultId = consultId == 0 ? throw new ArgumentNullException(nameof(consultId)) : consultId;
    }
}

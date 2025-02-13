using MediatR;
using Wio.LabConsult.Application.Features.Consults.Queries.VMs;

namespace Wio.LabConsult.Application.Features.Consults.Commands.DeleteConsult;

public class DeleteConsultCommand : IRequest<ConsultVm>
{
    public int ConsultId { get; set; }

    public DeleteConsultCommand(int consultId)
    {
        ConsultId = consultId == 0 ? throw new ArgumentException(nameof(consultId)) : consultId;
    }
}
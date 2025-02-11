using MediatR;
using Wio.LabConsult.Application.Features.Consults.Queries.VMs;
using Wio.LabConsult.Domain.Consults;

namespace Wio.LabConsult.Application.Features.Consults.Queries.GetConsultList;

//O que será desolvido? Uma lista de consultas
public class GetConsultListQuery : IRequest<IReadOnlyList<ConsultVm>>
{
}

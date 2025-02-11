using MediatR;
using Wio.LabConsult.Application.Features.Consults.Queries.VMs;
using Wio.LabConsult.Application.Features.Shared.Queries;
using Wio.LabConsult.Domain.Consults;

namespace Wio.LabConsult.Application.Features.Consults.Queries.PaginationConsults;

public class PaginationConsultsQuery : PaginationBaseQuery, IRequest<PaginationVm<ConsultVm>>
{
    public int? CategoryId { get; set; }
    public int? Rating { get; set; }
    public ConsultStatus? Status { get; set; }
}

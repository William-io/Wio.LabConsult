using MediatR;
using Wio.LabConsult.Application.Features.Reviews.Queries.Vms;
using Wio.LabConsult.Application.Features.Shared.Queries;

namespace Wio.LabConsult.Application.Features.Reviews.Queries.PaginationReviews;

public class PaginationReviewsQuery : PaginationBaseQuery, IRequest<PaginationVm<ReviewVm>>
{
    public int? ConsultId { get; set; }
}
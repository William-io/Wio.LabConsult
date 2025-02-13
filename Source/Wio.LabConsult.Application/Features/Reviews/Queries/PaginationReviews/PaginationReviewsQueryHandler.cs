using AutoMapper;
using MediatR;
using Wio.LabConsult.Application.Features.Reviews.Queries.Vms;
using Wio.LabConsult.Application.Features.Shared.Queries;
using Wio.LabConsult.Application.Persistence;
using Wio.LabConsult.Application.Specifications.Reviews;
using Wio.LabConsult.Domain.Reviews;

namespace Wio.LabConsult.Application.Features.Reviews.Queries.PaginationReviews;

public class PaginationReviewsQueryHandler : IRequestHandler<PaginationReviewsQuery, PaginationVm<ReviewVm>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PaginationReviewsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginationVm<ReviewVm>> Handle(PaginationReviewsQuery request, CancellationToken cancellationToken)
    {
        var reviewSpecificationParams = new ReviewSpecificationParams
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Search = request.Search,
            Sort = request.Sort,
            ConsultId = request.ConsultId
        };

        var spec = new ReviewSpecification(reviewSpecificationParams);
        var reviews = await _unitOfWork.Repository<Review>().GetAllWithSpec(spec);

        var specCount = new ReviewForCountingSpecification(reviewSpecificationParams);
        var totalReviews = await _unitOfWork.Repository<Review>().CountAsync(specCount);

        var rounded = Math.Ceiling(Convert.ToDecimal(totalReviews) / Convert.ToDecimal(request.PageSize));
        var totalPages = Convert.ToInt32(rounded);

        var data = _mapper.Map<IReadOnlyList<Review>, IReadOnlyList<ReviewVm>>(reviews);

        var reviewsByPage = reviews.Count();

        var pagination = new PaginationVm<ReviewVm>
        {
            Count = totalReviews,
            Data = data,
            PageCount = totalPages,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            ResultByPage = reviewsByPage
        };

        return pagination;
    }
}
using AutoMapper;
using MailKit.Search;
using MediatR;
using Wio.LabConsult.Application.Features.Requests.VMs;
using Wio.LabConsult.Application.Features.Shared.Queries;
using Wio.LabConsult.Application.Persistence;
using Wio.LabConsult.Application.Specifications.Requests;
using Wio.LabConsult.Domain.Requests;
namespace Wio.LabConsult.Application.Features.Requests.Queries.PaginationRequests;

public class PaginationRequestQueryHandler : IRequestHandler<PaginationRequestQuery, PaginationVm<RequestVm>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PaginationRequestQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginationVm<RequestVm>> Handle(PaginationRequestQuery request, CancellationToken cancellationToken)
    {
        var requestSpecificationParams = new RequestSpecificationParams
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Search = request.Search,
            Sort = request.Sort,
            Id = request.Id,
            Username = request.Username
        };

        var spec = new RequestSpecification(requestSpecificationParams);
        var requests = await _unitOfWork.Repository<Request>().GetAllWithSpec(spec);

        var specCount = new RequestForCountingSpecification(requestSpecificationParams);
        var totalRequests = await _unitOfWork.Repository<Request>().CountAsync(specCount);

        var rounded = Math.Ceiling(Convert.ToDecimal(totalRequests) / Convert.ToDecimal(request.PageSize));
        var totalPages = Convert.ToInt32(rounded);

        var data = _mapper.Map<IReadOnlyList<Request>, IReadOnlyList<RequestVm>>(requests);
        var requestsByPage = requests.Count();

        var pagination = new PaginationVm<RequestVm>
        {
            Count = totalRequests,
            Data = data,
            PageCount = totalPages,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            ResultByPage = requestsByPage
        };

        return pagination;
    }
}

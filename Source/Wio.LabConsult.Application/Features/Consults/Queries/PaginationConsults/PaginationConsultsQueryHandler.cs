using AutoMapper;
using MediatR;
using Wio.LabConsult.Application.Features.Consults.Queries.VMs;
using Wio.LabConsult.Application.Features.Shared.Queries;
using Wio.LabConsult.Application.Persistence;
using Wio.LabConsult.Application.Specifications.Consults;
using Wio.LabConsult.Domain.Consults;

namespace Wio.LabConsult.Application.Features.Consults.Queries.PaginationConsults;

public class PaginationConsultsQueryHandler : IRequestHandler<PaginationConsultsQuery, PaginationVm<ConsultVm>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PaginationConsultsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginationVm<ConsultVm>> Handle(PaginationConsultsQuery request, CancellationToken cancellationToken)
    {
        var consultSpecificationParams = new ConsultSpecificationParams
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Search = request.Search,
            Sort = request.Sort,
            CategoryId = request.CategoryId,
            Rating = request.Rating,
            Status = request.Status
        };

        var spec = new ConsultSpecification(consultSpecificationParams);
        var consults = await _unitOfWork.Repository<Consult>().GetAllWithSpec(spec);
        var specCount = new ConsultForCountingSpecification(consultSpecificationParams);
        var totalItems = await _unitOfWork.Repository<Consult>().CountAsync(specCount);

        var rounded = Math.Ceiling(Convert.ToDecimal(totalItems) / Convert.ToDecimal(request.PageSize));

        var data = _mapper.Map<IReadOnlyList<ConsultVm>>(consults);
        var consultsByPage = consults.Count();

        var pagination = new PaginationVm<ConsultVm>
        {
            Count = totalItems,
            Data = data,
            PageCount = totalItems,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            ResultByPage = consultsByPage
        };

        return pagination;
    }
}

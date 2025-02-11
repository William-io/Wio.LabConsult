using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using Wio.LabConsult.Application.Features.Consults.Queries.VMs;
using Wio.LabConsult.Application.Persistence;
using Wio.LabConsult.Domain.Consults;

namespace Wio.LabConsult.Application.Features.Consults.Queries.GetConsultList;

public class GetConsultListQueryHandler : IRequestHandler<GetConsultListQuery, IReadOnlyList<ConsultVm>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetConsultListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<ConsultVm>> Handle(GetConsultListQuery request, CancellationToken cancellationToken)
    {
        var includes = new List<Expression<Func<Consult, object>>>();
        includes.Add(x => x.Images!);
        includes.Add(x => x.Reviews!);

        var consults = await _unitOfWork.Repository<Consult>().GetAsync(
            null,
            x => x.OrderBy(y => y.Name),
            includes,
            true);

        var consultsVm = _mapper.Map<IReadOnlyList<ConsultVm>>(consults);

        return consultsVm;
    }
}

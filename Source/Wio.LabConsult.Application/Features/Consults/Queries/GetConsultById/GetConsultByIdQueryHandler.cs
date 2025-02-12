using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using Wio.LabConsult.Application.Features.Consults.Queries.GetConsultById;
using Wio.LabConsult.Application.Features.Consults.Queries.VMs;
using Wio.LabConsult.Application.Persistence;
using Wio.LabConsult.Domain.Consults;

namespace Wio.LabConsult.Application.Features.Consults.Queries;

public class GetConsultByIdQueryHandler : IRequestHandler<GetConsultByIdQuery, ConsultVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetConsultByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ConsultVm> Handle(GetConsultByIdQuery request, CancellationToken cancellationToken)
    {
        var includes = new List<Expression<Func<Consult, object>>>();
        includes.Add(x => x.Images!);
        includes.Add(x => x.Reviews!.OrderByDescending(x => x.CreatedDate));

        var consult = await _unitOfWork.Repository<Consult>().GetEntityAsync(
            x => x.Id == request.ConsultId, includes, true);

        return _mapper.Map<ConsultVm>(consult);
    }
}

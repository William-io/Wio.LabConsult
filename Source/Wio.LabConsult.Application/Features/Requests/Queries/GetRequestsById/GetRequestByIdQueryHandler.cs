using AutoMapper;
using MailKit.Search;
using MediatR;
using System.Linq.Expressions;
using Wio.LabConsult.Application.Features.Requests.VMs;
using Wio.LabConsult.Application.Persistence;
using Wio.LabConsult.Domain.Requests;

namespace Wio.LabConsult.Application.Features.Requests.Queries.GetRequestsById;

public class GetRequestByIdQueryHandler : IRequestHandler<GetRequestByIdQuery, RequestVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetRequestByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<RequestVm> Handle(GetRequestByIdQuery request, CancellationToken cancellationToken)
    {
        var includes = new List<Expression<Func<Request, object>>>();
        includes.Add(p => p.RequestItems!.OrderBy(x => x.Consult));
        includes.Add(p => p.RequestConfirmation!);

        var requestPatient = await _unitOfWork.Repository<Request>().GetEntityAsync(
            x => x.Id == request.RequestId,
            includes,
            false
        );

        return _mapper.Map<RequestVm>(requestPatient);
    }
}

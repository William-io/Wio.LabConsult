using AutoMapper;
using MediatR;
using Wio.LabConsult.Application.Features.Requests.VMs;
using Wio.LabConsult.Application.Persistence;
using Wio.LabConsult.Domain.Requests;

namespace Wio.LabConsult.Application.Features.Requests.Commands.UpdateRequest;

public class UpdateRequestCommandHandler : IRequestHandler<UpdateRequestCommand, RequestVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateRequestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<RequestVm> Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
    {
        var requestPatient = await _unitOfWork.Repository<Request>().GetByIdAsync(request.RequestId);
        requestPatient.Status = request.Status;

        _unitOfWork.Repository<Request>().UpdateEntity(requestPatient);
        var resultado = await _unitOfWork.Complete();

        if (resultado <= 0)
        {
            throw new Exception("Não foi possivel alterar o agendamento!");
        }

        return _mapper.Map<RequestVm>(requestPatient);
    }
}

using AutoMapper;
using MediatR;
using Wio.LabConsult.Application.Exceptions;
using Wio.LabConsult.Application.Features.Consults.Queries.VMs;
using Wio.LabConsult.Application.Persistence;
using Wio.LabConsult.Domain.Consults;

namespace Wio.LabConsult.Application.Features.Consults.Commands.DeleteConsult;

public class DeleteConsultCommandHandler : IRequestHandler<DeleteConsultCommand, ConsultVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteConsultCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ConsultVm> Handle(DeleteConsultCommand request, CancellationToken cancellationToken)
    {
        var consultToDelete = await _unitOfWork.Repository<Consult>().GetByIdAsync(request.ConsultId);

        if (consultToDelete is null)
        {
            throw new NotFoundException(nameof(Consult), request.ConsultId);
        }

        consultToDelete.Status = consultToDelete.Status == ConsultStatus.Inactive
                        ? ConsultStatus.Active : ConsultStatus.Inactive;

        await _unitOfWork.Repository<Consult>().UpdateAsync(consultToDelete);

        return _mapper.Map<ConsultVm>(consultToDelete);

    }
}
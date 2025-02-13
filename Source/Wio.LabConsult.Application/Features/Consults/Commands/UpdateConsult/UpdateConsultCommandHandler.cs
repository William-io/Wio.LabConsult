using AutoMapper;
using MediatR;

using Stripe;
using Wio.LabConsult.Application.Exceptions;
using Wio.LabConsult.Application.Features.Consults.Queries.VMs;
using Wio.LabConsult.Application.Persistence;
using Wio.LabConsult.Domain.Consults;
using Wio.LabConsult.Domain.Shared;

namespace Wio.LabConsult.Application.Features.Consults.Commands.UpdateConsult;

public class UpdateConsultCommandHandler : IRequestHandler<UpdateConsultCommand, ConsultVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateConsultCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ConsultVm> Handle(UpdateConsultCommand request, CancellationToken cancellationToken)
    {
        var consultToUpdate = await _unitOfWork.Repository<Consult>().GetByIdAsync(request.Id);
        if (consultToUpdate is null)
        {
            throw new NotFoundException(nameof(Consult), request.Id);
        }

        _mapper.Map(request, consultToUpdate, typeof(UpdateConsultCommand), typeof(Consult));
        await _unitOfWork.Repository<Consult>().UpdateAsync(consultToUpdate);

        if ((request.ImageUrls is not null) && request.ImageUrls.Count > 0)
        {
            var imagesToRemove = await _unitOfWork.Repository<Image>().GetAsync(
                x => x.ConsultId == request.Id
            );

            _unitOfWork.Repository<Image>().DeleteRange(imagesToRemove);

            request.ImageUrls.Select(c => { c.ConsultId = request.Id; return c; }).ToList();
            var images = _mapper.Map<List<Image>>(request.ImageUrls);
            _unitOfWork.Repository<Image>().AddRange(images);

            await _unitOfWork.Complete();
        }

        return _mapper.Map<ConsultVm>(consultToUpdate);
    }
}

using AutoMapper;
using MediatR;
using Wio.LabConsult.Application.Features.Consults.Queries.VMs;
using Wio.LabConsult.Application.Persistence;
using Wio.LabConsult.Domain.Consults;
using Wio.LabConsult.Domain.Shared;

namespace Wio.LabConsult.Application.Features.Consults.Commands.CreateConsult;

public class CreateConsultCommandHandler : IRequestHandler<CreateConsultCommand, ConsultVm>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public CreateConsultCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ConsultVm> Handle(CreateConsultCommand request, CancellationToken cancellationToken)
    {
        var productEntity = _mapper.Map<Consult>(request);
        await _unitOfWork.Repository<Consult>().AddAsync(productEntity);

        if ((request.ImageUrls is not null) && request.ImageUrls.Count > 0)
        {
            request.ImageUrls.Select(c => { c.ConsultId = productEntity.Id; return c; }).ToList();

            var images = _mapper.Map<List<Image>>(request.ImageUrls);
            _unitOfWork.Repository<Image>().AddRange(images);
            await _unitOfWork.Complete();
        }

        return _mapper.Map<ConsultVm>(productEntity);
    }
}
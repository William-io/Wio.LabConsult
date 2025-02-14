using AutoMapper;
using MediatR;
using Wio.LabConsult.Application.Features.Consults.Queries.VMs;
using Wio.LabConsult.Application.Features.Consults.Services.CEPs;
using Wio.LabConsult.Application.Persistence;
using Wio.LabConsult.Domain.Consults;
using Wio.LabConsult.Domain.Shared;

namespace Wio.LabConsult.Application.Features.Consults.Commands.CreateConsult;

public class CreateConsultCommandHandler : IRequestHandler<CreateConsultCommand, ConsultVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly CepService _cepService;

    public CreateConsultCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, CepService cepService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cepService = cepService;
    }

    public async Task<ConsultVm> Handle(CreateConsultCommand request, CancellationToken cancellationToken)
    {
        var consultEntity = _mapper.Map<Consult>(request);

        //Verificação e converter CEP para endereço
        if(_cepService.IsCep(request.Local) && request.Local is not null)
        {
            //No front input apenas de CEP
            var address = await _cepService.GetAddressFromCepAsync(request.Local);

            if(address is not null)
            {
                consultEntity.Address = new ClinicAddress(address.Localidade, address.Bairro, address.Logradouro, address.Uf);            
            }
        }

        await _unitOfWork.Repository<Consult>().AddAsync(consultEntity);

        if ((request.ImageUrls is not null) && request.ImageUrls.Count > 0)
        {
            request.ImageUrls.Select(c => { c.ConsultId = consultEntity.Id; return c; }).ToList();

            var images = _mapper.Map<List<Image>>(request.ImageUrls);
            _unitOfWork.Repository<Image>().AddRange(images);
            await _unitOfWork.Complete();
        }

        return _mapper.Map<ConsultVm>(consultEntity);
    }
}
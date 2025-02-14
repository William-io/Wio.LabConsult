using AutoMapper;
using MediatR;
using Wio.LabConsult.Application.Contracts.Identity;
using Wio.LabConsult.Application.Features.Addresses.Vms;
using Wio.LabConsult.Application.Persistence;
using Wio.LabConsult.Domain.Shared;

namespace Wio.LabConsult.Application.Features.Addresses.CreateAddress;

public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, AddressVm>
{
    private readonly IAuthService _authService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateAddressCommandHandler(IAuthService authService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _authService = authService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AddressVm> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {

        var addressRecord = await _unitOfWork.Repository<Address>().GetEntityAsync(
            x => x.Username == _authService.GetSessionUser(),
            null,
            false
        );

        if (addressRecord is null)
        {
            addressRecord = new Address
            {
                Country = request.Country,
                State = request.State,
                ZipCode = request.ZipCode,
                City = request.City,
                Street = request.Street,
                Username = _authService.GetSessionUser()
            };
            _unitOfWork.Repository<Address>().AddEntity(addressRecord);
        }
        else
        {
            addressRecord.Country = request.Country;
            addressRecord.State = request.State;
            addressRecord.ZipCode = request.ZipCode;
            addressRecord.City = request.City;
            addressRecord.Street = request.Street;

        }

        await _unitOfWork.Complete();

        return _mapper.Map<AddressVm>(addressRecord);

    }
}

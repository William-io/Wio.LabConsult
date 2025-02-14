using MediatR;
using Wio.LabConsult.Application.Features.Addresses.Vms;

namespace Wio.LabConsult.Application.Features.Addresses.CreateAddress;

public class CreateAddressCommand : IRequest<AddressVm>
{
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    //public string? Username { get; set; }
}

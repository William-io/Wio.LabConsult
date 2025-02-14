using AutoMapper;
using Wio.LabConsult.Application.Features.Addresses.Vms;
using Wio.LabConsult.Application.Features.Appointments.VMs;
using Wio.LabConsult.Application.Features.Categories.VMs;
using Wio.LabConsult.Application.Features.Consults.Commands.CreateConsult;
using Wio.LabConsult.Application.Features.Consults.Commands.UpdateConsult;
using Wio.LabConsult.Application.Features.Consults.Queries.VMs;
using Wio.LabConsult.Application.Features.Countries.VMs;
using Wio.LabConsult.Application.Features.Images.Queries.Vms;
using Wio.LabConsult.Application.Features.Reviews.Queries.Vms;
using Wio.LabConsult.Domain.Appointments;
using Wio.LabConsult.Domain.Categories;
using Wio.LabConsult.Domain.Consults;
using Wio.LabConsult.Domain.Requests;
using Wio.LabConsult.Domain.Reviews;
using Wio.LabConsult.Domain.Shared;

namespace Wio.LabConsult.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CreateMap<Source, Destination>();
        CreateMap<Consult, ConsultVm>()
            .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category!.Name))
            .ForMember(d => d.NumberReviews, opt => opt.MapFrom(s => s.Reviews == null ? 0 : s.Reviews.Count))
            .ForMember(dest => dest.Address!, opt => opt.MapFrom(src => src.Address!));


        CreateMap<Image, ImageVm>();
        CreateMap<Review, ReviewVm>();
        CreateMap<Country, CountryVm>();
        CreateMap<Category, CategoryVm>();
        CreateMap<CreateConsultCommand, Consult>();
        CreateMap<CreateConsultImageCommand, Image>();
        CreateMap<UpdateConsultCommand, Consult>();
        CreateMap<Appointment, AppointmentVm>().ForMember(c => c.AppointmenttId, x => x.MapFrom(a => a.AppointmentCartMasterId)); 
        CreateMap<AppointmentItem, AppointmentItemVm>();
        CreateMap<AppointmentItemVm, AppointmentItem>();
        CreateMap<Address, AddressVm>();


        CreateMap<Appointment, AppointmentVm>();
        CreateMap<AppointmentItem, AppointmentItemVm>();
        CreateMap<RequestConfirmation, AddressVm>();

    }
}

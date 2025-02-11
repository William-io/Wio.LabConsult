using AutoMapper;
using Wio.LabConsult.Application.Features.Consults.Queries.VMs;
using Wio.LabConsult.Application.Features.Images.Queries.Vms;
using Wio.LabConsult.Application.Features.Reviews.Queries.Vms;
using Wio.LabConsult.Domain.Consults;
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
            .ForMember(d => d.NumberReviews, opt => opt.MapFrom(s => s.Reviews == null ? 0 : s.Reviews.Count));

        CreateMap<Image, ImageVm>();
        CreateMap<Review, ReviewVm>();
    }
}

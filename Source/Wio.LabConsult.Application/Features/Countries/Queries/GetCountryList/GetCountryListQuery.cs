using MediatR;
using Wio.LabConsult.Application.Features.Countries.VMs;

namespace Wio.LabConsult.Application.Features.Countries.Queries.GetCountryList;

public class GetCountryListQuery : IRequest<IReadOnlyList<CountryVm>>
{
}
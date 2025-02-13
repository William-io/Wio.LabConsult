using MediatR;
using Wio.LabConsult.Application.Features.Categories.VMs;

namespace Wio.LabConsult.Application.Features.Categories.Queries.GetCategoryList;

public class GetCategoryListQuery : IRequest<IReadOnlyList<CategoryVm>>
{
}
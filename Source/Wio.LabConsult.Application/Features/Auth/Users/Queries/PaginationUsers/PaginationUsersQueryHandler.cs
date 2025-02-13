using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wio.LabConsult.Application.Features.Shared.Queries;
using Wio.LabConsult.Application.Persistence;
using Wio.LabConsult.Application.Specifications.Users;
using Wio.LabConsult.Domain.Users;

namespace Wio.LabConsult.Application.Features.Auth.Users.Queries.PaginationUsers;
public class PaginationUsersQueryHandler : IRequestHandler<PaginationUsersQuery, PaginationVm<User>>
{
    private readonly IUnitOfWork _unitOfWork;

    public PaginationUsersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginationVm<User>> Handle(PaginationUsersQuery request, CancellationToken cancellationToken)
    {
        var userSpecificationParams = new UserSpecificationParams
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Search = request.Search,
            Sort = request.Sort
        };

        var spec = new UserSpecification(userSpecificationParams);
        var users = await _unitOfWork.Repository<User>().GetAllWithSpec(spec);

        var specCount = new UserForCountingSpecification(userSpecificationParams);
        var totalUsers = await _unitOfWork.Repository<User>().CountAsync(specCount);

        var rounded = Math.Ceiling(Convert.ToDecimal(totalUsers) / Convert.ToDecimal(request.PageSize));
        var totalPages = Convert.ToInt32(rounded);

        var usersByPage = users.Count();

        var pagination = new PaginationVm<User>
        {
            Count = totalUsers,
            Data = users,
            PageCount = totalPages,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            ResultByPage = usersByPage
        };

        return pagination;

    }
}
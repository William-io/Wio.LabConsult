using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wio.LabConsult.Application.Features.Auth.Roles.Queries.GetRoles;

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<string>>
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public GetRolesQueryHandler(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<List<string>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleManager.Roles.ToListAsync();

        return roles.OrderBy(x => x.Name).Select(x => x.Name!).ToList<string>();
    }
}
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Wio.LabConsult.Application.Contracts.Identity;
using Wio.LabConsult.Application.Features.Addresses.Vms;
using Wio.LabConsult.Application.Features.Auth.Users.VMs;
using Wio.LabConsult.Application.Persistence;
using Wio.LabConsult.Domain.Shared;
using Wio.LabConsult.Domain.Users;

namespace Wio.LabConsult.Application.Features.Auth.Users.Queries.GetUserByToken;

public class GetUserByTokenHandler : IRequestHandler<GetUserByTokenQuery, AuthResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IAuthService _authService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserByTokenHandler(UserManager<User> userManager, IAuthService authService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _userManager = userManager;
        _authService = authService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AuthResponse> Handle(GetUserByTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(_authService.GetSessionUser());
        if (user is null)
        {
            throw new Exception("Usuario não autenticado");
        }

        if (!user.IsActive)
        {
            throw new Exception("Usuário está bloqueado, contactar admin");
        }

        var confirmation = await _unitOfWork.Repository<Address>().GetEntityAsync(
            x => x.Username == user.UserName);

        var roles = await _userManager.GetRolesAsync(user);

        return new AuthResponse
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            Phone = user.Phone,
            Email = user.Email,
            Username = user.UserName,
            Avatar = user.AvatarUrl,
            AddressVm = _mapper.Map<AddressVm>(confirmation),
            Token = _authService.CreateToken(user, roles),
            Roles = roles
        };
    }
}
using AutoMapper;
using CloudinaryDotNet;
using MediatR;
using Microsoft.AspNetCore.Identity;

using Wio.LabConsult.Application.Contracts.Identity;
using Wio.LabConsult.Application.Exceptions;
using Wio.LabConsult.Application.Features.Addresses.Vms;
using Wio.LabConsult.Application.Features.Auth.Users.VMs;
using Wio.LabConsult.Application.Persistence;
using Wio.LabConsult.Domain.Shared;
using Wio.LabConsult.Domain.Users;

namespace Wio.LabConsult.Application.Features.Auth.Users.Commands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public LoginUserCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, 
        IAuthService authService, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _authService = authService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email!);

        if (user is null)
            throw new NotFoundException(nameof(User), request.Email!);

        if (!user.IsActive)
            throw new Exception("Usuario está bloqueado, contactar administrador!");

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password!, false);

        if (!result.Succeeded)
            throw new Exception("As credenciais do usuário estão incorretas!");

        var userConfimation = await _unitOfWork.Repository<Address>().GetEntityAsync(
            x => x.Username == user.UserName);

        var roles = await _userManager.GetRolesAsync(user);

        var authResponse = new AuthResponse
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            Phone = user.Phone,
            Username = user.UserName,
            Email = user.Email,
            Avatar = user.AvatarUrl,
            AddressVm = _mapper.Map<AddressVm>(userConfimation),
            Token = _authService.CreateToken(user, roles),
        };

        return authResponse;

    }
}

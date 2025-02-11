using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wio.LabConsult.Application.Contracts.Identity;
using Wio.LabConsult.Application.Contracts.Services;
using Wio.LabConsult.Application.Models.ImageManagement;
using Wio.LabConsult.Application.Models.Token;
using Wio.LabConsult.Application.Persistence;
using Wio.LabConsult.Infrastructure.MessageImplementation;
using Wio.LabConsult.Infrastructure.Repositories;
using Wio.LabConsult.Infrastructure.Services.Auth;

namespace Wio.LabConsult.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IAuthService, AuthService>();

        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

        return services;
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wio.LabConsult.Application.Contracts.Services;
using Wio.LabConsult.Application.Models.ImageManagement;
using Wio.LabConsult.Application.Models.Token;
using Wio.LabConsult.Application.Persistence;
using Wio.LabConsult.Infrastructure.MessageImplementation;
using Wio.LabConsult.Infrastructure.Repositories;

namespace Wio.LabConsult.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
        services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}

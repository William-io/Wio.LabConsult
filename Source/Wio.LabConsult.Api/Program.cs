using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stripe;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;
using Wio.LabConsult.Application;
using Wio.LabConsult.Application.Contracts.Services;
using Wio.LabConsult.Application.Features.Consults.Queries.GetConsultList;
using Wio.LabConsult.Domain.Users;
using Wio.LabConsult.Infrastructure;
using Wio.LabConsult.Infrastructure.ImageCloudinary;
using Wio.LabConsult.Infrastructure.Persistence;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddDbContext<LabConsultDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"),
        bld => bld.MigrationsAssembly(typeof(LabConsultDbContext).Assembly.FullName))
    .EnableSensitiveDataLogging()
    .LogTo(Console.WriteLine, LogLevel.Information));

builder.Services.AddMediatR(typeof(GetConsultListQueryHandler).Assembly);
builder.Services.AddScoped<IManageImageService, ManageImageService>();

builder.Services.AddControllers(opt =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    opt.Filters.Add(new AuthorizeFilter(policy));
}).AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

IdentityBuilder identityBuilder = builder.Services.AddIdentityCore<User>();

identityBuilder.AddRoles<IdentityRole>().AddDefaultTokenProviders();
identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<User, IdentityRole>>();
identityBuilder.AddEntityFrameworkStores<LabConsultDbContext>();
identityBuilder.AddSignInManager<SignInManager<User>>();

//builder.Services.AddSingleton<TimeProvider>(TimeProvider.System);
builder.Services.TryAddSingleton<ISystemClock, SystemClock>();

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = key,
        ValidateAudience = false,
        ValidateIssuer = false
    };
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Wio.LabConsult.Api", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.\r\n\r\nEnter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string [] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers();

//Executar migration automatico ao iniciar a aplicação
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactore = services.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = services.GetRequiredService<LabConsultDbContext>();
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await context.Database.MigrateAsync();
        await LabConsultDbContextData.LoadDataAsync(context, userManager, roleManager, loggerFactore);
    }
    catch (Exception ex)
    {
        var logger = loggerFactore.CreateLogger<Program>();
        logger.LogError(ex, "Error ao criar um migration!");
    }
}

app.Run();

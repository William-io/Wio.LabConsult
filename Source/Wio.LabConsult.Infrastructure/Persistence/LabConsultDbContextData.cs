using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Wio.LabConsult.Domain.Categories;
using Wio.LabConsult.Domain.Consults;
using Wio.LabConsult.Domain.Shared;
using Wio.LabConsult.Domain.Users;
using Role = Wio.LabConsult.Application.Models.Authorization.Role;

namespace Wio.LabConsult.Infrastructure.Persistence;

public class LabConsultDbContextData
{
    public static async Task LoadDataAsync(LabConsultDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ILoggerFactory loggerFactory)
    {
		try
		{
			if(!roleManager.Roles.Any())
			{
                await roleManager.CreateAsync(new IdentityRole(Role.ADMIN));
                await roleManager.CreateAsync(new IdentityRole(Role.USER));
            }

            if (!userManager.Users.Any())
            {//Se não existe usuario
                var userAdmin = new User
                {
                    Name = "William",
                    LastName = "G.Silva",
                    Email = "capuletos@live.com",
                    UserName = "william.io",
                    Phone = "11999999999",
                    AvatarUrl = "https://png.com",
                };

                await userManager.CreateAsync(userAdmin, "PasswordCapuleto123$");
                await userManager.AddToRoleAsync(userAdmin, Role.ADMIN);

                var user = new User
                {
                    Name = "Julia",
                    LastName = "Cavalcante",
                    Email = "julia@live.com",
                    UserName = "julia.play",
                    Phone = "11999999999",
                    AvatarUrl = "https://png.com",
                };

                await userManager.CreateAsync(user, "PasswordJulia123$");
                await userManager.AddToRoleAsync(user, Role.USER);
            }

            if(!context.Categories!.Any())
            {
                var categoryData = File.ReadAllText("../Wio.LabConsult.Infrastructure/Data/category.json");
                var categories = JsonConvert.DeserializeObject<List<Category>>(categoryData);
                await context.Categories!.AddRangeAsync(categories!);
                await context.SaveChangesAsync();
            }

            if (!context.Consults!.Any())
            {
                var consultData = File.ReadAllText("../Wio.LabConsult.Infrastructure/Data/consult.json");
                var consults = JsonConvert.DeserializeObject<List<Consult>>(consultData);
                await context.Consults!.AddRangeAsync(consults!);
                await context.SaveChangesAsync();
            }

            if (!context.Countries!.Any())
            {
                var countryData = File.ReadAllText("../Wio.LabConsult.Infrastructure/Data/countries.json");
                var countries = JsonConvert.DeserializeObject<List<Country>>(countryData);
                await context.Countries!.AddRangeAsync(countries!);
                await context.SaveChangesAsync();
            }
        }
        catch (Exception e)
		{
			var logger = loggerFactory.CreateLogger<LabConsultDbContextData>();
			logger.LogError(e.Message);
		}
    }
}

using Microsoft.AspNetCore.Identity;
using ProductStore.Models;
using System.Net;

namespace ProductStore.Data
{
    public static class Seed
    {
        public static WebApplication SeedData(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using var context = scope.ServiceProvider.GetRequiredService<StoreContext>();

                try
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();


                    if (!context.Products.Any())
                    {
                        context.Products.AddRange(
                            new Product { Name = "Shanga" , Price = 49.99m },
                            new Product { Name = "Disk 5kg", Price = 22.99m },
                            new Product { Name = "Glovs", Price = 14.99m }
                            );

                        context.SaveChanges();
                    }
                }
                catch (Exception)
                {

                    throw;
                }

                return app;
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                //Roles
                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Customer>>();
                string adminUserEmail = "mineset@abv.bg";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new Customer();
                    newAdminUser.UserName = "Miogre";
                    newAdminUser.FirstName = "Pavel";
                    newAdminUser.LastName = "Pavlov";
                    newAdminUser.Address = "ul. Stefan Stambolov 15";
                    newAdminUser.Phone = "0892355623";
                    newAdminUser.Email = adminUserEmail;
                    newAdminUser.EmailConfirmed = true;


                    await userManager.CreateAsync(newAdminUser, "Aa@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "p.panushev@abv.bg";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new Customer();
                    newAppUser.UserName = "Test";
                    newAppUser.FirstName = "Ivan";
                    newAppUser.LastName = "Ivanov";
                    newAppUser.Address = "ul. Stefan Stambolov 34";
                    newAppUser.Phone = "0872356903";
                    newAppUser.Email = appUserEmail;
                    newAppUser.EmailConfirmed = true;

                    await userManager.CreateAsync(newAppUser, "Aa@1235?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

            }
        }
    }
}

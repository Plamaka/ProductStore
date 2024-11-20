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
                    context.Database.EnsureCreated();

                    if (!context.Customers.Any())
                    {
                        context.Customers.AddRange(
                            new Customer { FirstName = "Plamen", LastName = "Panushev", Address = "ul. Balak Malak", Phone = "0982549278"},
                            new Customer { FirstName = "Todor", LastName = "kostov", Address = "ul. Vanka Maska", Phone = "0982029278" },
                            new Customer { FirstName = "Yordan", LastName = "Vylkov", Address = "ul. Veba Stona", Phone = "0982549178" }
                            );
                        
                        context.SaveChanges();
                    }

                    if (!context.Orders.Any())
                    {
                        context.Orders.AddRange(
                            new Order { OrderPlaced = DateTime.Now, CustomerId = 1 },
                            new Order { OrderPlaced = DateTime.Now, CustomerId = 3 },
                            new Order { OrderPlaced = DateTime.Now, CustomerId = 2 }
                            );

                        context.SaveChanges();
                    }

                    if (!context.Products.Any())
                    {
                        context.Products.AddRange(
                            new Product { Name = "Shanga" , Price = 49.99m },
                            new Product { Name = "Disk 5kg", Price = 22.99m },
                            new Product { Name = "Glovs", Price = 14.99m }
                            );

                        context.SaveChanges();
                    }

                    if (!context.OrderDetails.Any())
                    {
                        context.OrderDetails.AddRange(
                            new OrderDetail { Quantity = 4, OrderId = 1,ProductId = 2 },
                            new OrderDetail { Quantity = 1, OrderId = 2, ProductId = 1 },
                            new OrderDetail { Quantity = 1, OrderId = 3, ProductId = 3 }
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
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "mineset@abv.bg";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser();
                    newAdminUser.Pace = 0;
                    newAdminUser.Mileage = 0;
                    newAdminUser.UserName = "Miogre";
                    newAdminUser.Email = adminUserEmail;
                    newAdminUser.EmailConfirmed = true;


                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "p.panushev@abv.bg";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser();
                    newAppUser.Pace = 0;
                    newAppUser.Mileage = 0;
                    newAppUser.UserName = "app-user";
                    newAppUser.Email = appUserEmail;
                    newAppUser.EmailConfirmed = true;

                    await userManager.CreateAsync( newAppUser, "Coding@12345?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

            }
        }
    }
}

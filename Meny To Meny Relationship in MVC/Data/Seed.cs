using Meny_To_Meny_Relationship_in_MVC.Models;
using Microsoft.AspNetCore.Identity;

namespace Meny_To_Meny_Relationship_in_MVC.Data
{
    public static class Seed
    {
        public static WebApplication SeedData(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using var context = scope.ServiceProvider.GetRequiredService<MenyToMenyContext>();

                try
                {
                    context.Database.EnsureCreated();

                    if (!context.Posts.Any())
                    {
                        context.Posts.AddRange(
                            new Post { Title = "This shit!", Description = "This is shit."},
                            new Post { Title = "That shit!", Description = "That is shit." },
                            new Post { Title = "What shit!", Description = "What is this shit!?" });
                        context.SaveChanges();
                    }

                    if (!context.Tags.Any())
                    {
                        context.Tags.AddRange(
                            new Tag { Title = "Shit"},
                            new Tag { Title = "What Shit"});
                        context.SaveChanges();
                    }

                    if (!context.PostTags.Any())
                    {
                        context.PostTags.AddRange(
                            new PostTag { PostId = 1, TagId = 1},
                            new PostTag { PostId = 2, TagId = 1 },
                            new PostTag { PostId = 3, TagId = 2 });
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
                    newAppUser.UserName = "Test";
                    newAppUser.Email = appUserEmail;
                    newAppUser.EmailConfirmed = true;

                    await userManager.CreateAsync(newAppUser, "Coding@12345?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

            }
        }
    }
}

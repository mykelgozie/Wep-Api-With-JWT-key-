using AspWebApi.Database;
using AspWebApi.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspWebApi.Seeder
{
    // seeding class
    public static class Seeder
    {

        public static async Task Seeding(AppDbcontext ctx, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            // chosse role
            ctx.Database.EnsureCreated();
            if (!roleManager.Roles.Any())
            {
                var listofRoles = new List<IdentityRole>()
                {
                    new IdentityRole("User")

                };

                foreach (var role in listofRoles)
                {
                    await roleManager.CreateAsync(role);
                }

            }
            // populate user 

            if (!userManager.Users.Any())
            {

                var listofUsers = new List<ApplicationUser>()
                {
                    // new user
                     new ApplicationUser{ UserName="mykel@gmail.com", Email = "mykel@gmail.com", LastName="Mike", FirstName="Mike", Photo = "~/images/avarta.jpg"},
                     new ApplicationUser{ UserName="frank@gmail.com", Email = "frank@gmail.com", LastName="Frank", FirstName="Frank", Photo = "~/images/avarta.jpg"}

                };

                // assign password
                foreach (var user in listofUsers)
                {

                    var result = await userManager.CreateAsync(user, "P@$$word1");

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "User");
                    }
                }

               
            }


        }


    }
}

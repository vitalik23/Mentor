using Mentor.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Mentor
{
    public class RoleInitializer
    {
        public static async System.Threading.Tasks.Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {


            string adminEmail = "admin@gmail.com";
            string password = "qwertyasdfgh";

            // role creating
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("student") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("student"));
            }
            if (await roleManager.FindByNameAsync("teacher") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("teacher"));
            }

            // connect admin role with admin user
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {

                // create admin user
                User admin = new User { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);

                // connect to this user admin role
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }

        }

    }
}

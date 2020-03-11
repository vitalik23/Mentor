using Mentor.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Mentor
{
    public class RoleInitializer
    {

        public const string ROLE_ADMIN = "admin";
        public const string ROLE_STUDENT = "student";
        public const string ROLE_TEACHER = "teacher";

        public static async System.Threading.Tasks.Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {


            string adminEmail = "admin@gmail.com";
            string password = "qwertyasdfgh";

            // role creating
            if (await roleManager.FindByNameAsync(ROLE_ADMIN) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(ROLE_ADMIN));
            }
            if (await roleManager.FindByNameAsync(ROLE_STUDENT) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(ROLE_STUDENT));
            }
            if (await roleManager.FindByNameAsync(ROLE_TEACHER) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(ROLE_TEACHER));
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
                    await userManager.AddToRoleAsync(admin, ROLE_ADMIN);
                }
            }

        }

    }
}

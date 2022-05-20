using JWTAKAR.Models;
using Microsoft.AspNetCore.Identity;

namespace JWTAKAR.Data
{
    public class SeedIdentity
    {

        public static void Seed(UserManager<User> userManager)
        {
            var user = new User()
            {
                UserName = "BerkayAkar",
                Email = "jwtuser@gmail.com"
            };

            if (userManager.FindByNameAsync("JwtUser").Result == null)
            {
                var identityResult = userManager.CreateAsync(user, "jwt123456").Result;
            }
        }
    }
}

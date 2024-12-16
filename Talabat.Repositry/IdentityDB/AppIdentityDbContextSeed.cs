using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;
using Talabat.Repositry.Data;

namespace Talabat.Repositry.IdentityDB
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Mahmoud Elsawy",
                    Email = "mahmoudbrazile9@gmail.com",
                    PhoneNumber = "1234567890",
                    UserName = "mahmoudbrazile9"
                };
                await userManager.CreateAsync(user,"Pa$$w0rd");
            }
        }
    }
}

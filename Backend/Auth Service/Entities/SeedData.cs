using System;
using System.Linq;
using Auth_Service.Database;
using Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Auth_Service.Entities
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new DatabaseContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DatabaseContext>>());
            // Look for any movies.
            if (context.Users.Any())
            {
                return; // DB has been seeded
            }

            var userMusify = new User
            {
                UserId = 1,
                DisplayName = "Musify",
                UserName = "musify",
                Password = "musify",
                Role = Role.Admin,
            };


            var userIcarus = new User
            {
                UserId = 2,
                DisplayName = "Icarus",
                UserName = "icarus",
                Password = "icarus",
                Role = Role.User,
            };

            userIcarus.AddFollowing(userMusify);
            //userMusify.AddFollower(userIcarus);

            context.Users.Add(userMusify);
            context.Users.Add(userIcarus);
            context.SaveChanges();
        }
    }
}

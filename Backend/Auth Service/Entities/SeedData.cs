﻿using System;
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

            context.Users.AddRange(
                new User()
                {
                    UserId = 1,
                    DisplayName = "Musify",
                    UserName = "musify",
                    Password = "musify",
                    Role = Role.Admin,

                }
            );
            context.SaveChanges();
        }
    }
}
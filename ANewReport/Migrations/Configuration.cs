namespace ANewReport.Migrations
{
    using ANewReport.Data;
    using ANewReport.Models;
    using ANewReport.Security;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ANewReport.Data.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppDbContext context)
        {
            // ---------- ROLES ----------
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(new[]
                {
            new Role { Name = "Admin" },
            new Role { Name = "User" }
        });

                context.SaveChanges();
            }

            // ---------- USERS ----------
            if (!context.Users.Any())
            {
                var adminRole = context.Roles.Single(r => r.Name == "Admin");
                var userRole = context.Roles.Single(r => r.Name == "User");
                context.Users.AddRange(new[] {
                    new User
                {
                    Username = "admin",
                    PasswordHash = PasswordHasher.Hash("admin123"), // make sure MaxLength >= 255
                    RoleId = adminRole.Id
                },new User{
                    Username = "user",
                    PasswordHash = PasswordHasher.Hash("user123"), // make sure MaxLength >= 255
                    RoleId = userRole.Id
                }
                });
                context.SaveChanges();
            }

            // ---------- EMPLOYEES ----------
            if (!context.Employees.Any())
            {
                context.Employees.AddRange(new[]
                {
            new Employee
            {
                Name = "Lobhas",
                City = "Kalyan",
                Salary = 200000m,
                HireDate = DateTime.Now
            },
            new Employee
            {
                Name = "Luffy",
                City = "East Blue",
                Salary = 250000m,
                HireDate = DateTime.Today.AddDays(-100)
            },
            new Employee
            {
                Name = "Zoro",
                City = "North Blue",
                Salary = 220000m,
                HireDate = DateTime.Today.AddDays(-70)
            }
        });

                context.SaveChanges();
            }
        }

    }
}

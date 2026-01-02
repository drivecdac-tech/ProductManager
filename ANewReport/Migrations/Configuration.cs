namespace ANewReport.Migrations
{
    using ANewReport.Data;
    using ANewReport.Models;
    using ANewReport.Security;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppDbContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.AddOrUpdate(
                    new Role { Name = "Admin" },
                    new Role { Name = "User" }
                );
            }

            context.SaveChanges();

            if (!context.Users.Any())
            {
                var adminRole = context.Roles.First(r => r.Name == "Admin");

                context.Users.Add(new User
                {
                    Username = "admin",
                    PasswordHash = PasswordHasher.Hash("admin123"),
                    RoleId = adminRole.Id
                });
            }
            context.SaveChanges();
            context.Employees.AddOrUpdate(
                new Employee {Name="Lobhas",City="Kalyan",Salary=200000,HireDate=DateTime.Now},
                new Employee {Name="Luffy",City="East Blue",Salary=25000000,HireDate=DateTime.Today.AddDays(-100)},
                new Employee {Name="Zoro",City="North Blue",Salary=22000000,HireDate=DateTime.Today.AddDays(-70) });
            context.SaveChanges();
        }
    }
}

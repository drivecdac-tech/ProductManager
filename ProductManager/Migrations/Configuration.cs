namespace ProductManager.Migrations
{
    using ProductManager.Data;
    using ProductManager.Models;
    using ProductManager.Security;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            // Register the MySQL migration SQL generator so EF migrations can produce MySQL-compatible SQL
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.EntityFramework.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(AppDbContext context)
        {
            // Roles
            if (!context.Roles.Any())
            {
                context.Roles.AddOrUpdate(
                    new Role { Name = "Admin" },
                    new Role { Name = "User" }
                );
            }

            context.SaveChanges();

            // Admin user
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
            context.Products.AddOrUpdate(
                new Product { Name = "Laptop", Price = 1200, Stock = 15, CreatedDate = DateTime.Now },
                new Product { Name = "Mouse", Price = 25, Stock = 120, CreatedDate = DateTime.Today.AddDays(-2) },
                new Product { Name = "Keyboard", Price = 75, Stock = 40, CreatedDate = DateTime.Today.AddDays(-5) }
            );
            context.SaveChanges();

            // Orders
            if (!context.Orders.Any())
            {
                var laptop = context.Products.First(p => p.Name == "Laptop");

                context.Orders.Add(new Order
                {
                    ProductId = laptop.Id,
                    Quantity = 2,
                    OrderDate = DateTime.Today
                });
            }

            context.SaveChanges();
        }
    }
}

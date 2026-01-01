using ProductManager.Models;
using System.Data.Entity;

namespace ProductManager.Data
{
    [DbConfigurationType(typeof(MySqlEFConfig))]
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("AppDbContext") { }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}

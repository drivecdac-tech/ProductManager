using ANewReport.Models;
using MySql.Data.EntityFramework;
using System.Data.Entity;

namespace ANewReport.Data
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]//MySQL EF6 Configuration
    public class AppDbContext:DbContext
    {
        public AppDbContext():base("name=AppDbContext"){ }//Connection string name in App.config

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

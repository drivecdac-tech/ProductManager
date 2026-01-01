using ProductManager.Data;
using ProductManager.Models;
using ProductManager.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Services
{
    public class UserService
    {
        public bool Register(string username, string password, string roleName)
        {
            using (var db = new AppDbContext())
            {
                if (db.Users.Any(u => u.Username == username))
                    return false;

                var role = db.Roles.First(r => r.Name == roleName);

                db.Users.Add(new User
                {
                    Username = username,
                    PasswordHash = PasswordHasher.Hash(password),
                    RoleId = role.Id
                });

                db.SaveChanges();
                return true;
            }
        }
    }
}

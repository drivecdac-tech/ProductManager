using ProductManager.Data;
using ProductManager.Models;
using ProductManager.Security;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace ProductManager.Services
{
    public class AuthService
    {
        public User Login(string username, string password)
        {
            using (var db = new AppDbContext())
            {
                var user = db.Users
                             .Include(u => u.Role)
                             .FirstOrDefault(u => u.Username == username);

                if (user == null)
                    return null;

                return PasswordHasher.Verify(password, user.PasswordHash)
                    ? user
                    : null;
            }
        }
    }
}

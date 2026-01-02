using ANewReport.Data;
using ANewReport.Models;
using ANewReport.Security;
using System.Data.Entity;
using System.Linq;

namespace ANewReport.Services
{
    public class AuthService
    {
        public User Login(string username, string password)
        {
            using(var context = new AppDbContext())
            {
                var user = context.Users.Include(u => u.Role)
                    .FirstOrDefault(u => u.Username == username);
                if(user==null)
                    return null;
                return PasswordHasher.Verify(password, user.PasswordHash) ? user : null;
            }
        }
    }
}

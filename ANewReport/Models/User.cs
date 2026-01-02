using System.ComponentModel.DataAnnotations;

namespace ANewReport.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public int RoleId { get; set; } // Foreign key
        public virtual Role Role { get; set; }// Navigation property to access Role details
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ANewReport.Models
{
    public class User
    {
        [Key]
        [Column(name: "id")]
        public int Id { get; set; }

        [Required]
        [Column(name: "username", TypeName = "varchar")]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [Column(name: "password_hash", TypeName = "varchar")]
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        [Column(name: "role_id")]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }

}

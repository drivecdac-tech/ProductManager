using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ANewReport.Models
{
    public class Role
    {
        [Key]
        [Column(name: "id")]
        public int Id { get; set; }

        [Required]
        [Column(name: "name", TypeName = "varchar")]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }

}

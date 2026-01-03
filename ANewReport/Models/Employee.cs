using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ANewReport.Models
{
    public class Employee
    {
        [Key]
        [Column(name: "id")]
        public int Id { get; set; }

        [Required]
        [Column(name: "name", TypeName = "varchar")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Column(name: "city", TypeName = "varchar")]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [Column(name: "hire_date")]
        public DateTime HireDate { get; set; }

        [Column(name: "salary")]
        public decimal Salary { get; set; }
    }

}

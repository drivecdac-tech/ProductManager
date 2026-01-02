using System;

namespace ANewReport.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
    }
}

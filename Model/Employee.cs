using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EmployeeDetails.Model
{
    public class Employee
    {
        public Employee(){}

        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department? Department { get; set; }

        
        public virtual ICollection<EmployeeProject>? EmployeeProjects { get; set; }



    }
}
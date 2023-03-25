using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmployeeDetails.HelperAttri;

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

        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Invalid mobile number format")]
        public string? MobileNumber { get; set; }

        public int DepartmentId { get; set; }
      
        public virtual Department? Department { get; set; }



    }
}
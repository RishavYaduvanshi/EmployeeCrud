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
        public string? Name { get; set; }

        [EmailAddress]
        [UniqueEmail]
        public string? Email { get; set; }

        public int DepartmentId { get; set; }
      
        public virtual Department? Department { get; set; }



    }
}
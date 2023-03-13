using System.ComponentModel.DataAnnotations;
using EmployeeDetails.DB;

namespace EmployeeDetails.HelperAttri
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = validationContext.GetService(typeof(EmployeeDbContext)) as EmployeeDbContext;
            var email = value as string;
            if (dbContext != null && dbContext.Employees.Any(p => p.Email == email))
            {
                return new ValidationResult("Email address already exists.");
            }
            return ValidationResult.Success;

        }
    }
}

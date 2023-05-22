using System.ComponentModel.DataAnnotations;

namespace EmployeeDetails.Dto
{
    public class SignIn
    {
        public string? Username { get; set; }
        [Required]
        public string Password { get; set; }

        public string? Email { get; set; }
    }
}

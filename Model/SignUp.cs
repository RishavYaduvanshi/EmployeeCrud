using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDetails.Model
{
    public class SignUp
    {
        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public ICollection<SignupRole>? SignupRoles {get;set;}  

    }
}

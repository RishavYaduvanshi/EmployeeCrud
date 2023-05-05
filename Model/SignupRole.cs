using Newtonsoft.Json;

namespace EmployeeDetails.Model
{
    public class SignupRole
    {

        public int? SignUpId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public SignUp? SignUp { get; set; }
        public int? RoleId { get; set; }

        public Role? Role { get; set; }
    }
}

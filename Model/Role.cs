using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeDetails.Model
{
    public class Role
    {
        public int RoleId { get; set; }
        public string? RoleName { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<SignupRole>? SignupRoles { get; set; }

    }
}   

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeDetails.Model
{
    public class Department
    {

        public int Id { get; set; }
        public string? Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Employee>? Employees { get; set; }

    }
}

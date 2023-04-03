using System.Text.Json.Serialization;

namespace EmployeeDetails.Model
{
    public class EmployeeProject
    {
        public int? EmployeeId { get; set; }

        [JsonIgnore]
        public Employee? Employee { get; set; }

        public int? ProjectId { get; set; }

        public Project? Project { get; set; }
    }
}

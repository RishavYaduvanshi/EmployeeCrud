using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace EmployeeDetails.Model
{
    public class Project
    {
        [Key]
        public int PId { get; set; }
        public string? PName { get; set; }
        public string? PDescription { get; set; }


        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<EmployeeProject>? EmployeeProjects { get; set; }

    }
}

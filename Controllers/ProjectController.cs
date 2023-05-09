
using EmployeeDetails.Model;
using EmployeeDetails.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeDetails.Controllers
{
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : Controller
    {

        private readonly IEmployeeRepsitory _employeeRepsitory;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IEmployeeRepsitory employeeRepsitory, IDepartmentRepository departmentRepository, IProjectRepository projectRepository)
        {
            _employeeRepsitory = employeeRepsitory;
            _departmentRepository = departmentRepository;
            _projectRepository = projectRepository;
        }

        [HttpGet]
        [Route("[action]/{id?}")]
        [Authorize(Roles = "admin")]


        public ActionResult<Project> GetProjectById(int id)
        {
           return  _projectRepository.GetProjectId(id);

        }



        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "admin,user")]
        public JsonResult GetAllProjects()
        {
            var projects = _projectRepository.GetAllProjects();
            return Json(projects);
        }


        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "admin")]
        public ActionResult<Project> AddProject(Project project)
        {
            
            _projectRepository.AddProject(project);
            return project;
        }

        [HttpPut]
        [Route("[action]/{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult<Project> UpdateProject(int id, Project project)
        {
            if (project.PName == null && project.PDescription == null)
                return BadRequest("Project Name and Project Description is null");
            if (_projectRepository.GetProjectId(id) == null)
                return BadRequest("Project Does Not Exist");
            Project proj = _projectRepository.UpdateProject(id, project);
            if ( proj== null)
                return BadRequest("Project already Exist");
            return proj;

        }

        [HttpDelete]
        [Route("[action]/{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult<Project> DeleteProject(int id)
        {
            Project project = _projectRepository.DeleteProject(id);
            if (project==null) return BadRequest("Project does not found");
            return project;
        }
    }
}

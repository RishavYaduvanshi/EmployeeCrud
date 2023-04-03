using EmployeeDetails.Dto;
using EmployeeDetails.Model;
using EmployeeDetails.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDetails.Controllers
{

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

        public ActionResult<Project> GetProjectById(int id)
        {
           return  _projectRepository.GetProjectId(id);

        }

        [HttpGet]
        [Route("[action]")]
        public JsonResult GetAllProjects()
        {
            var projects = _projectRepository.GetAllProjects();
            return Json(projects);
        }


        [HttpPost]
        [Route("[action]")]
        public ActionResult<Project> AddProject(Project project)
        {
            
            _projectRepository.AddProject(project);
            return project;
        }

        [HttpPut]
        [Route("[action]/{id}")]
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
    }
}

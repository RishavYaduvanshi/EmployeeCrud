using EmployeeDetails.Model;
using EmployeeDetails.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDetails.Controllers
{
    [ApiController]
   
    [Route("[controller]")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;

        }
        [HttpGet]
        [Route("[action]/{id?}")]
        public JsonResult GetDepartment(int? id)
        {
            Department dep = departmentRepository.GetDepartmentID(id??0);
            return Json(dep);
        }
        [HttpGet]
        [Route("[action]")]
        public JsonResult GetAllDepartments()
        {
            IEnumerable<Department> dep =  departmentRepository.GetAllDepartment();
            return Json(dep) ;
        }
        [HttpPost]
        [Route("[action]")]
        public ActionResult<Department> AddDepartment(Department department) 
        {
            departmentRepository.AddDepartment(department);
            return department;
        }

        [HttpDelete]
        [Route("[action]/{id}")]

        public ActionResult<Department> DeleteDepartment(int id)
        {
            Department dep =  departmentRepository.DeleteDepartment(id);
            if(dep == null)
            {
                return BadRequest("Department not Found");
            }
            return dep;
        }

    }
}

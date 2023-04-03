using EmployeeDetails.Dto;
using EmployeeDetails.Model;
using EmployeeDetails.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;

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
        [Route("[action]/{id}")]
        public ActionResult<Department> GetDepartment(int id)
        {
            if (id == 0 )
                return BadRequest("Department Not Found");
            Department dep = departmentRepository.GetDepartmentID(id);
            if (dep == null)
                return BadRequest("Department Not Found");
            return dep;
        }
        [HttpGet]
        [Route("[action]")]
        public JsonResult GetAllDepartments()
        {
            IEnumerable<Department> dep =  departmentRepository.GetAllDepartment();
            var deplist =dep.ToList();
            deplist.Reverse();
            return Json(deplist) ;
        }
        [HttpPost]
        [Route("[action]")]
        public ActionResult<Department> AddDepartment(Department department) 
        {

            if (departmentRepository.AddDepartment(department) == null)
                return BadRequest("Department already exist");
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

        [HttpPut]
        [Route("[action]/{id}")]
        public ActionResult<Department> UpdateDepartment(int id,Department dep)
        {
            if (dep.Name == null)
                return BadRequest("Department name is null");
            if(departmentRepository.GetDepartmentID(id) == null)
                return BadRequest("Department Id does not exist");
            Department deapartment = departmentRepository.UpdateDepartment(id, dep);
            if (deapartment == null) return BadRequest("Department Name already exist");
            return deapartment;
        }
    }
}

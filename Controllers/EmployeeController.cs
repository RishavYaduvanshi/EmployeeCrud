using EmployeeDetails.Model;
using EmployeeDetails.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDetails.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepsitory _employeerepository;


        public EmployeeController(IEmployeeRepsitory employeerepsitory)
        {
            _employeerepository = employeerepsitory;
        }
        [HttpGet]
        [Route("[action]/{id?}")]
        public JsonResult GetEmployeeByID(int? id)
        { 
           Employee emp =  _employeerepository.GetEmployeeById(id??1);
            Console.WriteLine(Json(emp));
            return Json(emp);
        }
        [HttpGet]
        [Route("[action]")]
        public JsonResult GetAllEmployee()
        {
            IEnumerable<Employee> emp = _employeerepository.GetAllEmployees();
            return Json(emp);   
        }

        [HttpPost]
        [Route("[action]")]
        public JsonResult AddEmployee(Employee emp)
        {
            _employeerepository.AddEmployee(emp);
            return Json(emp);
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public ActionResult<Employee> UpdateEmployee(int id, [FromBody] Employee emp)
        {
        
            var updatedEmployee = _employeerepository.UpdateEmployee(id,emp);

            if (updatedEmployee == null)
            {
                return NotFound();
            }

            return (updatedEmployee);
        }
        [HttpDelete]
        [Route("[action]/{id}")]
        public ActionResult<Employee> DeleteEmployee(int id)
        {
            var employee = _employeerepository.DeleteEmployee(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

    }
}
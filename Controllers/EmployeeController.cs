using EmployeeDetails.Dto;
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
        private readonly IDepartmentRepository _departmentrepository;


        public EmployeeController(IEmployeeRepsitory employeerepsitory,IDepartmentRepository departmentRepository)
        {
            _employeerepository = employeerepsitory;
            _departmentrepository = departmentRepository;
        }
        [HttpGet]
        [Route("[action]/{id?}")]
        public ActionResult<Employee> GetEmployeeByID(int? id)
        { 
           Employee emp =  _employeerepository.GetEmployeeById(id??1);
           if(emp == null) return NotFound("Employee id does not match");
           emp.Department =  _departmentrepository.GetDepartmentID(emp.DepartmentId);
           Console.WriteLine(Json(emp));
           return emp;
        }
        [HttpGet]
        [Route("[action]")]
        public JsonResult GetAllEmployee()
        {

            IEnumerable<Employee> emp = _employeerepository.GetAllEmployees();
            var emplist = emp.Cast<Employee>().ToList();
            foreach (var employee in emplist)
            {
                employee.Department = _departmentrepository.GetDepartmentID(employee.DepartmentId);
            }
            
            return Json(emplist);   
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<Employee> AddEmployee(EmployeeDto emp)
        {
            if (emp.Name == null || emp.Email == null || emp.DepartmentId == 0)
            {
                return BadRequest("Employee name or email or Department is null.");
            }
            
            Employee empl = new Employee()
            {
                Name = emp.Name,
                Department = emp.DepartmentId == null ? null : _departmentrepository.GetDepartmentID((int)emp.DepartmentId),
                Email = emp.Email,
            };

            if (_employeerepository.AddEmployee(empl) == null)
                return BadRequest("Email already exists in database.");
            
            _employeerepository.AddEmployee(empl);
            return empl;
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public ActionResult<Employee> UpdateEmployee(int id, [FromBody] EmployeeDto emp)
        {
            if (emp.Name == null && emp.Email == null && emp.DepartmentId == null)
            {
                return BadRequest("Employee name or email or Department is null.");
            }
            Employee empl = new Employee()
            {
                Name = emp.Name,
                Email = emp.Email,
                DepartmentId =(int) emp.DepartmentId
            };

            var updatedEmployee = _employeerepository.UpdateEmployee(id,empl);

            if (updatedEmployee == null)
            {
                return NotFound("Employee does not Exist");
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
                return NotFound("Employee does not Exist");
            }
            return employee;
        }

    }
}
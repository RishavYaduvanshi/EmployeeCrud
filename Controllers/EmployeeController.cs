using EmployeeDetails.Dto;
using EmployeeDetails.Model;
using EmployeeDetails.Repository;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace EmployeeDetails.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepsitory _employeerepository;
        private readonly IDepartmentRepository _departmentrepository;
        private readonly IEmpProjRepository _empProjRepository;
        private readonly IProjectRepository _projectRepository;




        public EmployeeController(IEmployeeRepsitory employeerepsitory, IDepartmentRepository departmentRepository,IEmpProjRepository empProjRepository,IProjectRepository projectRepository)
        {
            _employeerepository = employeerepsitory;
            _departmentrepository = departmentRepository;
            _empProjRepository = empProjRepository;
            _projectRepository = projectRepository;
        }
        [HttpGet]
        [Route("[action]/{id?}")]
        public ActionResult<Employee> GetEmployeeByID(int? id)
        {
            Employee emp = _employeerepository.GetEmployeeById(id ?? 1);
            if (emp == null) return NotFound("Employee id does not match");

            emp.Department = _departmentrepository.GetDepartmentID(emp.DepartmentId);

            emp.EmployeeProjects = new List<EmployeeProject>();
            List<int> projlist =  _empProjRepository.GetById(emp.Id);
            
            foreach (var proj in projlist)
            {
                
                EmployeeProject ep = new EmployeeProject()
                {
                    EmployeeId = emp.Id,
                    ProjectId = proj,
                    Project = _projectRepository.GetProjectId(proj)
                };
                emp.EmployeeProjects.Add(ep);
            }

            return emp;
        }
        [HttpGet]
        [Route("[action]")]
        public JsonResult GetAllEmployee()
        {

            IEnumerable<Employee> emp = _employeerepository.GetAllEmployees();
            var emplist = emp.ToList();
            foreach (var employee in emplist)
            {
                employee.Department = _departmentrepository.GetDepartmentID(employee.DepartmentId);
            }
            foreach(var employee in emplist)
            {
                employee.EmployeeProjects = new List<EmployeeProject>();

                foreach (var proj in _empProjRepository.GetById(employee.Id))
                {
                    EmployeeProject ep = new EmployeeProject()
                    {
                        EmployeeId = employee.Id,
                        ProjectId = proj,
                        Project = _projectRepository.GetProjectId(proj)

                    };
                    employee.EmployeeProjects.Add(ep);
                }
            }
            emplist.Reverse();
            return Json(emplist);
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<Employee> AddEmployee(EmployeeDto emp)
        {
            var department = _departmentrepository.GetAllDepartment().FirstOrDefault(d => d.Name == emp.DepartmentName);
            

            if (department != null)
            {
                emp.DepartmentId = department.Id;
            }
            else
            {
                return BadRequest("Department Does not exist");
            }
            if ( emp.MobileNumber.Length != 10 && !Regex.IsMatch(emp.MobileNumber, @"^(\+?\d{1,3}[- ]?)?\d{10}$"))
            {
                return BadRequest("Enter correct Mobile number");
            }

            if (emp.FirstName == null || emp.LastName == null || emp.MobileNumber == null || emp.Email == null || emp.DepartmentId == 0)
            {
                return BadRequest("Employee Data is not Filled.");
            }
            if (_departmentrepository.GetDepartmentID((int)emp.DepartmentId) == null)
                return BadRequest("Department Not Found");
          

            Employee empl = new Employee()
            {
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                MobileNumber = emp.MobileNumber,
                Department = emp.DepartmentId == 0 ? null : _departmentrepository.GetDepartmentID((int)emp.DepartmentId),
                Email = emp.Email,
                EmployeeProjects = new List<EmployeeProject>()
            };

            if (_employeerepository.AddEmployee(empl) == null)
                return BadRequest("Email already exists in database.");

            _employeerepository.AddEmployee(empl);

            foreach (var proj in emp.ProjectIds)
            {
                EmployeeProject ep = new EmployeeProject()
                {
                    EmployeeId = empl.Id,
                    ProjectId = proj,
                    Project = _projectRepository.GetProjectId(proj)
            };
                empl.EmployeeProjects.Add(ep);
                _empProjRepository.AddEmpProj(ep);
            }

           
            

            return empl;
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public ActionResult<Employee> UpdateEmployee(int id, [FromBody] EmployeeDto emp)
        {
            if (emp.DepartmentName != null)
            {
                var department = _departmentrepository.GetAllDepartment().FirstOrDefault(d => d.Name == emp.DepartmentName);
                if (department != null)
                {
                    emp.DepartmentId = department.Id;
                }
                else
                {
                    return BadRequest("Department does not exist");
                }
            }
             
            if (emp.FirstName == null && emp.LastName == null && emp.Email == null && emp.MobileNumber == null && emp.DepartmentId == 0)
            {
                return BadRequest("Employee name or email or Department is null.");
            }
            
            Employee empl = new Employee()
            {
                FirstName = emp.FirstName,
                LastName  = emp.LastName,
                MobileNumber = emp.MobileNumber,
                Email = emp.Email,
                DepartmentId = (int)emp.DepartmentId
            };

            var updatedEmployee = _employeerepository.UpdateEmployee(id, empl);
            if (updatedEmployee == null)
            {
                return NotFound("Email is Duplicate");
            }
            if (emp.ProjectIds != null)
            {
                if (_empProjRepository.UpdateProjectList(emp.ProjectIds,id) == null)
                    return BadRequest("Project Does not exist");
                updatedEmployee.EmployeeProjects = new List<EmployeeProject>();
                foreach (var proj in emp.ProjectIds)
                {

                    EmployeeProject ep = new EmployeeProject()
                    {
                        EmployeeId = emp.Id,
                        ProjectId = proj,
                        Project = _projectRepository.GetProjectId(proj)
                    };
                    updatedEmployee.EmployeeProjects.Add(ep);
                }
            }
            else
            {
                updatedEmployee.EmployeeProjects = new List<EmployeeProject>();
                List<int> projlist = _empProjRepository.GetById(id);
                foreach (var proj in projlist)
                {

                    EmployeeProject ep = new EmployeeProject()
                    {
                        EmployeeId = id,
                        ProjectId = proj,
                        Project = _projectRepository.GetProjectId(proj)
                    };
                    updatedEmployee.EmployeeProjects.Add(ep);
                }
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
                return NotFound("Employee does not Exist.");
            }
            return employee;
        }
       

       
 

    }
}
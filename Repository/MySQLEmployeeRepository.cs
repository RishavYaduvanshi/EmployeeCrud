using EmployeeDetails.DB;
using EmployeeDetails.Model;

namespace EmployeeDetails.Repository
{
    public class MySQLEmployeeRepository : IEmployeeRepsitory
    {
        private readonly EmployeeDbContext context;

        public MySQLEmployeeRepository(EmployeeDbContext context)
        {
            this.context = context;
        }

        public Employee AddEmployee(Employee employee)
        {

            if (context.Employees.Any(u => u.Email == employee.Email))
            {
                return null;
            }
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employee DeleteEmployee(int id)
        {
            Employee emp = context.Employees.Find(id);
            if (emp != null)
            {
                context.Employees.Remove(emp);
                context.SaveChanges();
            }
            return emp;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return context.Employees;
        }

        public Employee GetEmployeeById(int Id)
        {
            return context.Employees.Find(Id);
        }

        public Employee UpdateEmployee(int id, Employee employeeChange)
        {
            var employee = context.Employees.Find(id);
            if (employee.Email != employeeChange.Email)
            {
                if (context.Employees.Any(u => u.Email == employeeChange.Email))
                {
                    return null;
                }
            }
            if (employee != null)
            {
                if (!string.IsNullOrEmpty(employeeChange.Name))
                    employee.Name = employeeChange.Name;
                if (!string.IsNullOrEmpty(employeeChange.Email))
                    employee.Email = employeeChange.Email;
                if (employeeChange.DepartmentId != 0 && employee.DepartmentId != employeeChange.DepartmentId)
                {
                    var department = context.Departments.Find(employeeChange.DepartmentId);
                    if (department != null)
                    {
                        employee.DepartmentId = employeeChange.DepartmentId;
                    }
                    else
                        return null;

                }
                context.SaveChanges();
            }
            return employee;
        }
    }
    }

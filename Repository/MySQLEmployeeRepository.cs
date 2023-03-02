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

            if (employee != null && !string.IsNullOrEmpty(employeeChange.Name))
            {
                employee.Name = employeeChange.Name;
                context.SaveChanges();
            }

            return employee;
        }
    }
}

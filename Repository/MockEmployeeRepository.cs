using EmployeeDetails.Model;

namespace EmployeeDetails.Repository
{
    public class MockEmployeeRepository : IEmployeeRepsitory
    {
        private List<Employee> _employees;

        public MockEmployeeRepository()
        {
            _employees = new List<Employee>()
            {
                
            };


        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employees;
        }

        public Employee GetEmployeeById(int Id)
        {
            return _employees.FirstOrDefault(x => x.Id == Id);
        }

        public Employee AddEmployee(Employee employee)
        {
            int nextId = _employees.Any() ? _employees.Max(e => e.Id) + 1 : 1;
            employee.Id = nextId;
            _employees.Add(employee);
            return employee;
        }

        public Employee DeleteEmployee(int Id)
        {
            Employee emp = _employees.FirstOrDefault(x => x.Id == Id);
            if (emp != null)
            {
                _employees.Remove(emp);
            }
            return emp;
        }


        public Employee UpdateEmployee(int id, Employee employee)
        {
            Employee emp = _employees.FirstOrDefault(x => x.Id == employee.Id);
            if (emp != null)
            {
                emp.FirstName = employee.FirstName;
            }
            return emp;
        }


    }
}

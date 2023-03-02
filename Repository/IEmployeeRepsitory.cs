using EmployeeDetails.Model;

namespace EmployeeDetails.Repository
{
    public interface IEmployeeRepsitory
    {
        Employee GetEmployeeById(int Id);
        IEnumerable<Employee> GetAllEmployees();

        Employee AddEmployee(Employee employee);
        Employee UpdateEmployee(int id, Employee employee);
        Employee DeleteEmployee(int id);
    }
}

using EmployeeDetails.Model;

namespace EmployeeDetails.Repository
{
    public interface IDepartmentRepository
    {
        public Department? GetDepartmentID(int ID);
        public IEnumerable<Department> GetAllDepartment();
        public Department AddDepartment(Department department);
        public Department UpdateDepartment(int id, Department department);
        public Department DeleteDepartment(int id);

    }
}

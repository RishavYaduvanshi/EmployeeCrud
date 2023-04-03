using EmployeeDetails.DB;
using EmployeeDetails.Model;

namespace EmployeeDetails.Repository
{
    public class MySQLDepartmentRepository : IDepartmentRepository
    {
        private readonly EmployeeDbContext context;

        public MySQLDepartmentRepository(EmployeeDbContext context)
        {
            this.context = context;
        }

        public Department AddDepartment(Department department)
        {

            if(context.Departments.Any(x => x.Name.ToLower() == department.Name.ToLower()))
                return null;
            context.Departments.Add(department);
            context.SaveChanges();
            return department;
        }

        public Department DeleteDepartment(int id)
        {
            Department dep = context.Departments.Find(id);
            if (dep != null)
            {
                context.Departments.Remove(dep);
                context.SaveChanges();
            }
            return dep;
        }

        public IEnumerable<Department> GetAllDepartment()
        {
            return context.Departments;
        }

        public Department? GetDepartmentID(int ID)
        {
            return context.Departments.Find(ID);

        }

        public Department UpdateDepartment(int id, Department departmentChange)
        {
      
            Department dep = GetDepartmentID(id);
            if ( dep.Name.ToLower() != departmentChange.Name.ToLower() && context.Departments.Any(x => x.Name.ToLower() == departmentChange.Name.ToLower()))
                return null;
            if (!string.IsNullOrEmpty(departmentChange.Name))
                dep.Name = departmentChange.Name;
            context.SaveChanges();
            return dep;

        }

    }
}

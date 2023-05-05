using EmployeeDetails.DB;
using EmployeeDetails.Model;

namespace EmployeeDetails.Repository
{
    public class MySQLRoleRepo : IRoleRepo
    {
        private readonly EmployeeDbContext _employeeDbContext;
        public MySQLRoleRepo(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }
        public Role AddRole(Role role)
        {
            _employeeDbContext.Roles.Add(role);
            _employeeDbContext.SaveChanges();
            return role;
        }

        public Role DeleteRole(Role role)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetAllRole()
        {
            return _employeeDbContext.Roles;
        }

        public Role GetRoleById(int id)
        {
            return _employeeDbContext.Roles.Find(id);
        }

        public Role UpdateRole(Role role)
        {
            throw new NotImplementedException();
        }
    }
}

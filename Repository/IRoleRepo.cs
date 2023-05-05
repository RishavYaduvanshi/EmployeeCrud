using EmployeeDetails.Model;

namespace EmployeeDetails.Repository
{
    public interface IRoleRepo
    {
        public Role AddRole(Role role);

        public Role UpdateRole(Role role);

        public Role DeleteRole(Role role);

        public IEnumerable<Role> GetAllRole();

        public Role GetRoleById(int id);





    }
}

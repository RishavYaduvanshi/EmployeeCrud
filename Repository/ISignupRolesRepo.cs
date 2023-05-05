using EmployeeDetails.Model;

namespace EmployeeDetails.Repository
{
    public interface ISignupRolesRepo
    {
        public void AddSignupRole(SignupRole sr);

        public IEnumerable<SignupRole> GetAllSignupRole();

        public List<int> GetSignupRoleById(int id);

        public List<int> GetById(int id);
    }
}

using EmployeeDetails.DB;
using EmployeeDetails.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDetails.Repository
{
    public class MySQLSignupRoleRepo : ISignupRolesRepo
    {
        private readonly EmployeeDbContext _dbContext;
        public MySQLSignupRoleRepo(EmployeeDbContext employeeDbContext)
        {
            _dbContext = employeeDbContext;
        }
        public void AddSignupRole(SignupRole sr)
        {
            _dbContext.SignUpsRoles.Add(sr);
            _dbContext.SaveChanges();
        }

        public List<int> GetById(int id)
        {
            var signUplist = _dbContext.SignUpsRoles
                            .Where(ep => ep.SignUpId == id)
                            .Select(ep => (int)ep.RoleId)
                            .ToList();
            return signUplist;

        }


        public IEnumerable<SignupRole> GetAllSignupRole()
        {
            return _dbContext.SignUpsRoles;
        }

        public List<int> GetSignupRoleById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

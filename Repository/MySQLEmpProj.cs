using EmployeeDetails.DB;
using EmployeeDetails.Dto;
using EmployeeDetails.Model;
using Microsoft.VisualBasic;

namespace EmployeeDetails.Repository
{
    public class MySQLEmpProj : IEmpProjRepository
    {
        private readonly EmployeeDbContext _dbContext;
        public MySQLEmpProj(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddEmpProj(EmployeeProject ep)
        {
            _dbContext.Add(ep);
            _dbContext.SaveChanges();
        }

        public IEnumerable<EmployeeProject> GetAllEmpProj()
        {
            return _dbContext.EmployeeProjects;
        }

        public List<int> GetById(int id)
        {
            var projlist = _dbContext.EmployeeProjects
                            .Where(ep => ep.EmployeeId == id)
                            .Select(ep => (int)ep.ProjectId)
                            .ToList();
           return projlist;

        }
        public List<int> UpdateProjectList(List<int> projid, int empid)
        {
            int id = empid;
            var allProjectIds = _dbContext.EmployeeProjects
                                .Select(ep => (int)ep.ProjectId)
                                .ToHashSet();
            if (!projid.All(id => allProjectIds.Contains(id)))
            {
                return null;
            }
            var employeeProjectsToDelete = _dbContext.EmployeeProjects
                                           .Where(ep => ep.EmployeeId == empid);

            _dbContext.EmployeeProjects.RemoveRange(employeeProjectsToDelete);
            _dbContext.SaveChanges();


            var updateProj = projid.Select(pid => new EmployeeProject
            {
                EmployeeId = empid,
                ProjectId = pid
            });
            _dbContext.EmployeeProjects.AddRange(updateProj);
            _dbContext.SaveChanges();

            return projid;
        }


    }
}

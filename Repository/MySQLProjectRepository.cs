using EmployeeDetails.DB;
using EmployeeDetails.Model;
using System.Runtime.Intrinsics.Arm;

namespace EmployeeDetails.Repository
{
    public class MySQLProjectRepository : IProjectRepository
    {
        private readonly EmployeeDbContext _dbContext;
        public MySQLProjectRepository(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
                
        }
        public Project AddProject(Project project)
        {
            if (_dbContext.Projects.Any(p => p.PName == project.PName))
                return null;
            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();
            return project;
        }

        public Project DeleteProject(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return _dbContext.Projects;
        }

        public Project? GetProjectId(int Id)
        {
            return _dbContext.Projects.Find(Id);
        }

        public Project UpdateProject(int id ,Project projectChange)
        {
            Project proj = _dbContext.Projects.Find(id);
            if (proj == null)
                return null;
            if (projectChange.PName!= null && proj.PName.ToLower() != projectChange.PName.ToLower() && _dbContext.Projects.Any(x => x.PName.ToLower() == projectChange.PName.ToLower()))
                return null;
            
            if (!string.IsNullOrEmpty(projectChange.PName))
                proj.PName = projectChange.PName;
            if (!string.IsNullOrEmpty(projectChange.PDescription))
                proj.PDescription = projectChange.PDescription;
            _dbContext.SaveChanges();
            return proj;
        }
    }
}

using EmployeeDetails.Model;

namespace EmployeeDetails.Repository
{
    public interface IProjectRepository
    {
        public Project? GetProjectId(int Id);
        public IEnumerable<Project> GetAllProjects();
        public Project AddProject(Project project);
        public Project UpdateProject(int id ,Project project);
        public Project DeleteProject(int Id);
       
    }
}

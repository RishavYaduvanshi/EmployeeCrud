using EmployeeDetails.Model;

namespace EmployeeDetails.Repository
{
    public interface IEmpProjRepository
    {
        public void AddEmpProj(EmployeeProject ep);

        public IEnumerable<EmployeeProject> GetAllEmpProj();

        public List<int> GetById(int id);
        public List<int> UpdateProjectList(List<int> list, int id);
    }
}

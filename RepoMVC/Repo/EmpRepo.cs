using RepoMVC.Models;

namespace RepoMVC.Repo
{
    public interface EmpRepo
    {
        void AddEmp(Emp e);
        List<Emp> GetAllEmps();
        void RemoveEmp(int id);

        void UpdateEmp(Emp e);

        //List<Emp> SearchEmps(string searchTerm);
    }
}

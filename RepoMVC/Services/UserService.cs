using RepoMVC.Data;
using RepoMVC.Models;
using RepoMVC.Repo;

namespace RepoMVC.Services
{
    public class UserService:UserRepo
    {
        private readonly ApplicationDbContext db;
        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }



        public void SignUp(User us)
        {
            db.users.Add(us);
            db.SaveChanges();
        }
    }
}

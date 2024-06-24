using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoMVC.Data;
using RepoMVC.Models;
using RepoMVC.Repo;


namespace RepoMVC.Controllers
{
    [Authorize]
    public class EmpController : Controller
    {   private readonly EmpRepo empRepo;
        private readonly ApplicationDbContext db;
        public EmpController(EmpRepo empRepo,ApplicationDbContext db)
        {
           this.db = db;
            this.empRepo = empRepo;
        }

        public IActionResult Index()
        {
            var dt = empRepo.GetAllEmps();
            return View(dt);
        }

        //public IActionResult Index(string searchp)
        //{
        //    List<Emp> dt;
        //    if (!string.IsNullOrEmpty(searchp))
        //    {
        //        dt = empRepo.SearchEmps(searchp);
        //    }
        //    else
        //    {
        //        dt = empRepo.GetAllEmps();
        //    }
        //    return View(dt);
        //}
        public IActionResult AddEmp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddEmp(Emp emp)
        { 
            if(ModelState.IsValid)
            {
                empRepo.AddEmp(emp);
                TempData["msg"] = "Employee Added Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
         
        }

        public IActionResult RemoveEmp(int id)
        {   empRepo.RemoveEmp(id);
            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult EditEmp(int id)
        {
            var emp = empRepo.GetAllEmps().FirstOrDefault(e => e.Id == id);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }

        [HttpPost]
        public IActionResult EditEmp(Emp emp)
        {
            if (ModelState.IsValid)
            {
                empRepo.UpdateEmp(emp);
                TempData["msg"] = "Employee Updated Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(emp);
            }
        }

       
    }
}

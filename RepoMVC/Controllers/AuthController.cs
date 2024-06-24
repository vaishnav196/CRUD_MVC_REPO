using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RepoMVC.Data;
using RepoMVC.Models;
using RepoMVC.Repo;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace RepoMVC.Controllers
{
   
    public class AuthController : Controller
    {
       
       
        UserRepo repo;
        private readonly ApplicationDbContext db;
        public AuthController(UserRepo repo, ApplicationDbContext db)
        {
            this.repo = repo;
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User us)
        {
            repo.SignUp(us);
            TempData["success"] = "User Registred Successfully";
            return RedirectToAction("Signin");
        }

        public IActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signin(Login log)
        {
            var data = db.users.Where(x => x.Username.Equals(log.Username) && x.Password.Equals(log.Password)).SingleOrDefault();
            if (data != null)
            {
                var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, data.Username) }, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                HttpContext.Session.SetString("Username", data.Username);
                return RedirectToAction("Index", "Emp");
            }
            else
            {
                TempData["error"] = "Invalid Credentials!!";
                return RedirectToAction("Signin");
            }
        }

        public IActionResult Signout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var storedcookies = Request.Cookies.Keys;
            foreach (var cookie in storedcookies)
            {
                Response.Cookies.Delete(cookie);
            }
            return RedirectToAction("Signin");
        }
    }
}

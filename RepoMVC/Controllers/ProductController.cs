using Microsoft.AspNetCore.Mvc;
using RepoMVC.Data;
using RepoMVC.Models;

namespace RepoMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment env;
        public ProductController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

        public IActionResult Index()
        {
            var data = db.products.ToList();
            return View(data);
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(ProductModel pm)
        {
            var path = env.WebRootPath;
            var filePath = "Content/Images/" + pm.Pimg.FileName; //placing the image
            var fullPath = Path.Combine(path, filePath);    //combine the path with wwwroot folder
            UploadFile(pm.Pimg, fullPath);

            var p = new Product()
            {
                Pname = pm.Pname,
                Pcat = pm.Pcat,
                Pimg = filePath,
                Price = pm.Price,
            };
            db.Add(p);
            db.SaveChanges();
            TempData["success"] = "Product Added Successfully";
            return RedirectToAction("Index");
        }

        public void UploadFile(IFormFile file, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
        }

        public IActionResult DeleteProduct(int id)
        {
            var d = db.products.Find(id);
            db.products.Remove(d);
            db.SaveChanges();
            TempData["error"] = "Product Deleted Successfully!!";
            return RedirectToAction("Index");
        }
    }
}
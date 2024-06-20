using System.ComponentModel.DataAnnotations;

namespace RepoMVC.Models
{
    public class ProductModel
    {
        [Key]
        public int Pid { get; set; }
        public string Pname { get; set; }
        public string Pcat { get; set; }
        public IFormFile Pimg { get; set; }

        public double Price { get; set; }
    }
}

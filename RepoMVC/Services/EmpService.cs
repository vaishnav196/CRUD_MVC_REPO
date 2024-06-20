using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RepoMVC.Data;
using RepoMVC.Models;
using RepoMVC.Repo;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RepoMVC.Services
{
    public class EmpService : EmpRepo
    {
        private readonly ApplicationDbContext db;
        public EmpService(ApplicationDbContext db) 
        { 
            this.db = db;

        }
        public void AddEmp(Emp e)
        {
            db.emps.Add(e);
            db.SaveChanges();   
        }

        public List<Emp> GetAllEmps()
        {
           var data=db.emps.ToList();
            return data;
        }


        public void RemoveEmp(int id)
        {
            var data = db.emps.Find(id);
            if(data!=null)
            {
                db.emps.Remove(data);   
                db.SaveChanges();   
            }
        }

        //public List<Emp> SearchEmps(string searchTerm)
        //{
        //    List<Emp> data;
        //    if (string.IsNullOrEmpty(searchTerm))
        //    {
        //        data = db.emps.ToList();
        //    }
        //    else
        //    {
        //        data=db.emps.Where(x=>x.Name.Contains(searchTerm) || x.Salary.ToString().Contains(searchTerm)).ToList();
        //    }
        //    return data;
        //}

        public void UpdateEmp(Emp e)
        {

            var existingEmp = db.emps.Find(e.Id);
            if (existingEmp != null)
            {
                existingEmp.Name = e.Name;
                existingEmp.Salary = e.Salary;
                // Update other properties as necessary

                db.emps.Update(existingEmp);
                db.SaveChanges();
            }
        }
      
    }
}

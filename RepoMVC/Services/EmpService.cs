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

        //public List<Emp> SearchEmps(string searchTerm)
        //{
        //    if (string.IsNullOrEmpty(searchTerm))
        //    {
        //        return db.emps.ToList();
        //    }
        //    else
        //    {
        //        return db.emps
        //            .Where(x => x.Name.Contains(searchTerm) || x.Salary.ToString().Contains(searchTerm))
        //            .ToList();
        //    }
        //}

        //List<Emp> EmpRepo.DeleteSelectedEmps(List<int> ids)
        //{
        //    if (ids != null && ids.Count > 0)
        //    {
        //        var empsToDelete = db.emps.Where(e => ids.Contains(e.Id)).ToList();
        //        db.emps.RemoveRange(empsToDelete);
        //        db.SaveChanges();
        //    }

        //}
    }
}

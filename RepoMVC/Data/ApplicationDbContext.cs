﻿using Microsoft.EntityFrameworkCore;
using RepoMVC.Models;
using System.Collections.Generic;

namespace RepoMVC.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options
        ) : base(options)
        {


        }
        public DbSet<Emp> emps { get; set; }
        public DbSet<Product> products { get; set; }
    }
}

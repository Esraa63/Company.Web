using Company.Data.Entites;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Company.Data.Contexts
{
    public class CompanyDbContext:DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext > options): base(options) 
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("server =.; database=CompanyMVC01; trusted_connection=true; TrustServerCertificate=True;");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //modelBuilder.Entity<BaseEntity>().HasQueryFilter(x => !x.IsDeleted);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Employee> Employees { get; set; }  
        public DbSet<Department> Departments { get; set; }   
    }   
}

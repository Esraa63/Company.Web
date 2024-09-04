using Company.Data.Contexts;
using Company.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyDbContext _context;

        public UnitOfWork(CompanyDbContext context)
        {
            _context = context;
            DepartmentRepository = new DepartmentRepository(context);
            EmployeeRepository = new EmployeeRepository(context);   
        }
        public IDepartmentRepository DepartmentRepository { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEmployeeRepository EmployeeRepository { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Complete() => _context.SaveChanges();
        
    }
}

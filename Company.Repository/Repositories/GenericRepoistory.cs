using Company.Data.Contexts;
using Company.Data.Entites;
using Company.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Repositories
{
    public class GenericRepoistory<T> : IGenericRepoistory<T> where T : BaseEntity
    {
        private readonly CompanyDbContext _context;

        public GenericRepoistory(CompanyDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)=>
            _context.Set<T>().Add(entity);
        public void Delete(T entity)=>
            _context.Set<T>().Remove(entity);
		//.AsNoTracking().ToList()

		public IEnumerable<T> GetAll()=> 
            _context.Set<T>().ToList();
        

        public T GetById(int id)=>
            _context.Set<T>().Find(id);
          

        public void Update(T entity)=>  
            _context.Set<T>().Update(entity);
        
    }
}

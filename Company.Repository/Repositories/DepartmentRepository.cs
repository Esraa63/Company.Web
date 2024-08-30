using Company.Data.Contexts;
using Company.Data.Entitis;
using Company.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Repositories
{
    public class DepartmentRepository : GenericRepoistory<Department> , IDepartmentRepository
    {
        public DepartmentRepository(CompanyDbContext context) : base(context) 
        {
        }
    }
}

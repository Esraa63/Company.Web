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
    public class EmployeeRepository : GenericRepoistory<Employee> , IEmployeeRepository
    {
        public EmployeeRepository(CompanyDbContext context) : base(context)
        {
        }

        public Employee GetEmployeeByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetEmployeesByAddress(string address)
        {
            throw new NotImplementedException();
        }
    }
}

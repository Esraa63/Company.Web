﻿using Company.Data.Entitis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Interfaces
{
    public interface IEmployeeRepository : IGenericRepoistory<Employee>
    {
        Employee GetEmployeeByName (string name);
        IEnumerable<Employee> GetEmployeesByAddress (string address);
    }
}

﻿using Company.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Interfaces
{
    public interface IEmployeeRepository : IGenericRepoistory<Employee>
    {
        IEnumerable<Employee> GetEmployeeByName (string name);
        IEnumerable<Employee> GetEmployeesByAddress (string address);
    }
}

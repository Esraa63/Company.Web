using Company.Data.Entitis;
using Company.Repository.Interfaces;
using Company.Service.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Employee employee)
        {
            
        }

        public void Delete(Employee employee)
        {
            _unitOfWork.EmployeeRepository.Delete(employee);
            _unitOfWork.Complete();
        }
        IEnumerable<Employee> IEmployeeService.GetAll()
        {
            var employees = _unitOfWork.EmployeeRepository.GetAll();
            return employees;
        }

        public Employee GetById(int? id)
        {
            if (id is null)
                return null;
                    var employee = _unitOfWork.EmployeeRepository.GetById(id.Value);
            if (employee is null)
                return null;
            return employee;
        }
        public void Update(Employee employee)
        {
            _unitOfWork.EmployeeRepository.Update(employee);
            _unitOfWork.Complete();
        }

        public IEnumerable<Employee> GetEmployeeByName(string name)
        => _unitOfWork.EmployeeRepository.GetEmployeeByName(name);
    }
}

using AutoMapper;
using Company.Data.Entites;
using Company.Repository.Interfaces;
using Company.Service.Helper;
using Company.Service.InterFaces;
using Company.Service.InterFaces.Employee.Dto;
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
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(EmployeeDto employeeDto)
        {
            //Manual Mapping
            //Employee employee = new Employee
            //{
            //    Address = employeeDto.Address,
            //    Age = employeeDto.Age,
            //    DepartmentId = employeeDto.DepartmentId,
            //    Email = employeeDto.Email,
            //    ImageUrl = employeeDto.ImageUrl,
            //    Name = employeeDto.Name,
            //    PhoneNumber = employeeDto.PhoneNumber,
            //    Salary = employeeDto.Salary
            //};

            employeeDto.ImageUrl = DocumentSettings.UploadFile(employeeDto.Image, "Images");
            Employee employee = _mapper.Map<Employee>(employeeDto);       
            _unitOfWork.EmployeeRepository.Add(employee);
            _unitOfWork.Complete();
        }

        public void Delete(EmployeeDto employeeDto)
        {
            //Manual Mapping
            //Employee employee = new Employee
            //{
            //    Address = employeeDto.Address,
            //    Age = employeeDto.Age,
            //    DepartmentId = employeeDto.DepartmentId,
            //    Email = employeeDto.Email,
            //    ImageUrl = employeeDto.ImageUrl,
            //    Name = employeeDto.Name,
            //    PhoneNumber = employeeDto.PhoneNumber,
            //    Salary = employeeDto.Salary
            //};
            Employee employee = _mapper.Map<Employee>(employeeDto);
            _unitOfWork.EmployeeRepository.Delete(employee);
            _unitOfWork.Complete();
        }
        IEnumerable<EmployeeDto> IEmployeeService.GetAll()
        {
            //var mappedEmployess = employees.Select(x => new EmployeeDto
            //{
            //    DepartmentId=x.DepartmentId,
            //    Address = x.Address,
            //    Age = x.Age,
            //    Email = x.Email,
            //    ImageUrl = x.ImageUrl,
            //    Name = x.Name,
            //    PhoneNumber = x.PhoneNumber,
            //    Salary = x.Salary,
            //    Id = x.Id
            //});
            var employees = _unitOfWork.EmployeeRepository.GetAll();
            var mappedEmployess = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return mappedEmployess;
        }

        public EmployeeDto GetById(int? id)
        {
            if (id is null)
                return null;
                    var employee = _unitOfWork.EmployeeRepository.GetById(id.Value);
            if (employee is null)
                return null;
            //EmployeeDto employeeDto = new EmployeeDto
            //{
            //    Address = employee.Address,
            //    Age = employee.Age,
            //    DepartmentId = employee.DepartmentId,
            //    Email = employee.Email,
            //    ImageUrl = employee.ImageUrl,
            //    Name = employee.Name,
            //    PhoneNumber = employee.PhoneNumber,
            //    Salary = employee.Salary,
            //    Id = employee.Id,
            //    CreateAt=employee.CreateAt
            //};
            EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }
        //public void Update(EmployeeDto employeeDto)
        //{
        //    //Manual Mapping
        //    Employee employee = new Employee
        //    {
        //        Address = employeeDto.Address,
        //        Age = employeeDto.Age,
        //        DepartmentId = employeeDto.DepartmentId,
        //        Email = employeeDto.Email,
        //        ImageUrl = employeeDto.ImageUrl,
        //        Name = employeeDto.Name,
        //        PhoneNumber = employeeDto.PhoneNumber,
        //        Salary = employeeDto.Salary
        //    };
        //    _unitOfWork.EmployeeRepository.Update(employee);
        //    _unitOfWork.Complete();
        //}

        public IEnumerable<EmployeeDto> GetEmployeeByName(string name)
        {
          var employees = _unitOfWork.EmployeeRepository.GetEmployeeByName(name);
            //var mappedEmployess = employees.Select(x => new EmployeeDto
            //{
            //    DepartmentId = x.DepartmentId,
            //    Address = x.Address,
            //    Age = x.Age,
            //    Email = x.Email,
            //    ImageUrl = x.ImageUrl,
            //    Name = x.Name,
            //    PhoneNumber = x.PhoneNumber,
            //    Salary = x.Salary,
            //    Id = x.Id
            //});
            IEnumerable<EmployeeDto> mappedEmployess = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return mappedEmployess;
        }

        public void Update(EmployeeDto employee)
        {
            throw new NotImplementedException();
        }
    }
}

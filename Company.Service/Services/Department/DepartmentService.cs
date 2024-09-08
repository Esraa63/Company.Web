using AutoMapper;
using Company.Data.Entites;
using Company.Repository.Interfaces;
using Company.Service.InterFaces;
using Company.Service.InterFaces.Department.Dto;
using Company.Service.InterFaces.Employee.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public void Add(DepartmentDto departmentDto)
        {
            //var mappedDepartment = new Department
            //{
            //    Code = departmentDto.Code,
            //    Name = departmentDto.Name,
            //    CreateAt = DateTime.Now
            //};
            var mappedDepartment = _mapper.Map<Department>(departmentDto);
            _unitOfWork.DepartmentRepository.Add(mappedDepartment);
            _unitOfWork.Complete();
        }
       
        public void Delete(DepartmentDto departmentDto)
        {
            //var mappedDepartment = new Department
            //{
            //    Code = departmentDto.Code,
            //    Name = departmentDto.Name,
            //    CreateAt = DateTime.Now
            //};
            var mappedDepartment = _mapper.Map<Department>(departmentDto);
            _unitOfWork.DepartmentRepository.Delete(mappedDepartment);
            _unitOfWork.Complete();
        }

        public IEnumerable<DepartmentDto> GetAll()
        {
            //var mappedDepartments = departments.Select(x => new DepartmentDto
            //{
            //    Code= x.Code,
            //    Name= x.Name,
            //    CreateAt= x.CreateAt
            //});
            var departments = _unitOfWork.DepartmentRepository.GetAll(); ;
            var mappedDepartments =_mapper.Map<IEnumerable<DepartmentDto>>(departments);
            return mappedDepartments;
        }

        public DepartmentDto GetById(int? id)
        {
            if (id is null)
                return null;
            var department = _unitOfWork.DepartmentRepository.GetById(id.Value);
            if (department is null)
                return null;
            //DepartmentDto departmentDto = new DepartmentDto
            //{
            //    Code= department.Code,
            //    Name= department.Name,
            //    CreateAt = department.CreateAt
            //};
            var mappedDepartments = _mapper.Map<DepartmentDto>(department);
            return mappedDepartments;
        }

        public void Update(DepartmentDto department)
        {
            //_unitOfWork.DepartmentRepository.Update(department);
            //_unitOfWork.Complete();
        }

    }
}

using API.DTOs.RequestDTOs;
using API.DTOs.ResponseDTOs;
using AutoMapper;
using Infrastructure.GenericRepository;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace API.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IGenericRepository<Department> _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public DepartmentService(IGenericRepository<Department> departmentRepository
            , IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(RequestDepartmentDTO requestDepartmentDto)
        {
            var department = _mapper.Map<Department>(requestDepartmentDto);
            return await _departmentRepository.AddAsync(department);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var employees = await _employeeRepository.GetByDepartmentId(id);
            if (employees.Count() > 0)
            {
                foreach (var employee in employees)
                {
                    employee.DepartmentId = null;
                    await _employeeRepository.UpdateAsync(employee);
                }
            }
            return await _departmentRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ResponseDepartmentDTO>> GetAllAsync()
        {
            var departments = await _departmentRepository.GetAllAsync();
            var departmentDtos = _mapper.Map<IEnumerable<ResponseDepartmentDTO>>(departments);
            return departmentDtos;
        }

        public async Task<ResponseDepartmentDTO?> GetByIdAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            var departmentDto = _mapper.Map<ResponseDepartmentDTO>(department);
            return departmentDto;
        }

        public Task<int> UpdateAsync(int id, RequestDepartmentDTO requestDepartmentDto)
        {
            var department = _mapper.Map<Department>(requestDepartmentDto);
            department.Id = id;
            return _departmentRepository.UpdateAsync(department);
        }
    }
}

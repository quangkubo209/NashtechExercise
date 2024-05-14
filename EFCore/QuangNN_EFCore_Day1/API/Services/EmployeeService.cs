using API.DTOs.RequestDTOs;
using API.DTOs.ResponseDTOs;
using AutoMapper;
using Infrastructure.GenericRepository;
using Infrastructure.Models;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericRepository<Employee> _employeeRepository;
        private readonly IGenericRepository<Salaries> _salaryRepository;
        private readonly IGenericRepository<Project_Employee> _projectEmployeeRepository;
        private  readonly IGenericRepository<Department> _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IGenericRepository<Employee> employeeRepository,
                               IGenericRepository<Salaries> salaryRepository,
                               IGenericRepository<Project_Employee> projectEmployeeRepository,
                               IGenericRepository<Department> departmentRepository,
                               IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _salaryRepository = salaryRepository;
            _projectEmployeeRepository = projectEmployeeRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(RequestEmployeeDTO requestEmployeeDto)
        {
            var employee = _mapper.Map<Employee>(requestEmployeeDto);
            return await _employeeRepository.AddAsync(employee);
        }

        public async Task<int> DeleteAsync(int id)
        {
            //delete salary 
            var salary = await _salaryRepository.GetByIdAsync(id);

                await _salaryRepository.DeleteAsync(salary.Id);

            return await _employeeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ResponseEmployeeDTO>> GetAllAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            var employeeDtos = _mapper.Map<IEnumerable<ResponseEmployeeDTO>>(employees);
            return employeeDtos;
        }

        public async Task<ResponseEmployeeDTO?> GetByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            var employeeDto = _mapper.Map<ResponseEmployeeDTO>(employee);
            return employeeDto;
        }

        public async Task<int> UpdateAsync(int id, RequestEmployeeDTO requestEmployeeDto)
        {
            var employee = _mapper.Map<Employee>(requestEmployeeDto);
            employee.Id = id;
            return await _employeeRepository.UpdateAsync(employee);
        }

        // Liệt kê tất cả nhân viên và dự án của họ (sử dụng left join)
        public async Task<IEnumerable<ResponseEmployeeProjectDTO>> GetAllEmployeesWithProjects()
        {
            var employees = await _employeeRepository.GetAllAsync();
            var projectEmployees = await _projectEmployeeRepository.GetAllAsync();

            var query = from emp in employees
                        join pe in projectEmployees on emp.Id equals pe.EmployeeId into empProj
                        from ep in empProj.DefaultIfEmpty()
                        select new ResponseEmployeeProjectDTO
                        {
                            EmployeeId = emp.Id,
                            EmployeeName = emp.Name,
                            ProjectId = ep != null ? ep.ProjectId : 0
                        };

            return query.ToList();
        }

        public async Task<IEnumerable<ResponseEmployeeDepartmentDTO>> GetAllEmployeesWithDepartments()
        {
            var employees = await _employeeRepository.GetAllAsync();
            var departments = await _departmentRepository.GetAllAsync();

            var query = from emp in employees
                        join dep in departments on emp.DepartmentId equals dep.Id
                        select new ResponseEmployeeDepartmentDTO
                        {
                            EmployeeId = emp.Id,
                            EmployeeName = emp.Name,
                            DepartmentName = dep.Name
                        };

            return query.ToList();
        }

        public async Task<IEnumerable<ResponseEmployeeDTO>> GetEmployeeWithFilter(int minSalary, string minJoinedDate)
        {
            DateTime dateMin = DateTime.ParseExact(minJoinedDate, "dd/MM/yyyy", null);
            var employees = await _employeeRepository.GetAllAsync();

            var filteredEmployees = employees
                .Where(emp => emp.JoinedDate >= dateMin)
                .Select(emp => new ResponseEmployeeDTO
                {
                    Name = emp.Name,
                    Salary = emp.Salary.Salary
                })
                .Where(emp => emp.Salary > minSalary)
                .ToList();

            return filteredEmployees;
        }
    }
}

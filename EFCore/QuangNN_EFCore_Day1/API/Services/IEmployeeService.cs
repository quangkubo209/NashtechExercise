using API.DTOs.RequestDTOs;
using API.DTOs.ResponseDTOs;

namespace API.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<ResponseEmployeeDTO>> GetAllAsync();
        Task<int> AddAsync(RequestEmployeeDTO requestEmployeeDTO);
        Task<int> UpdateAsync(int id, RequestEmployeeDTO requestEmployeeDTO);
        Task<int> DeleteAsync(int id);
        Task<ResponseEmployeeDTO?> GetByIdAsync(int id);
        Task<IEnumerable<ResponseEmployeeDepartmentDTO>> GetAllEmployeesWithDepartments();

        Task<IEnumerable<ResponseEmployeeProjectDTO>> GetAllEmployeesWithProjects();

        Task<IEnumerable<ResponseEmployeeDTO>> GetEmployeeWithFilter(int minSalary, string minJoinedDate);


    }
}

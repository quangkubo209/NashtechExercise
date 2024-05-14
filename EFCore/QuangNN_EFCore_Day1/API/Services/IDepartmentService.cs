using API.DTOs.RequestDTOs;
using API.DTOs.ResponseDTOs;

namespace API.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<ResponseDepartmentDTO>> GetAllAsync();
        Task<int> AddAsync(RequestDepartmentDTO requestDepartmentDto);
        Task<int> UpdateAsync(int id, RequestDepartmentDTO requestDepartmentDto);
        Task<int> DeleteAsync(int id);
        Task<ResponseDepartmentDTO?> GetByIdAsync(int id);
    }
}

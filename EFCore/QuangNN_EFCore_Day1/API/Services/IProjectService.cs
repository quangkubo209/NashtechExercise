using API.DTOs.RequestDTOs;
using API.DTOs.ResponseDTOs;

namespace API.Services
{
    public interface IProjectService
    {
        Task<int> AddAsync(RequestProjectDTO requestProjectDto);
        Task<int> DeleteAsync(int id);
        Task<IEnumerable<ResponseProjectDTO>> GetAllAsync();
        Task<ResponseProjectDTO?> GetByIdAsync(int id);
        Task<int> UpdateAsync(int id, RequestProjectDTO requestProjectDto);
    }
}


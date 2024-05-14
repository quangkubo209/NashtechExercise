using API.DTOs.RequestDTOs;
using API.DTOs.ResponseDTOs;

namespace API.Services
{
    public interface ISalariesService
    {
        Task<int> AddAsync(RequestSalariesDto requestSalaryDto);
        Task<int> DeleteAsync(int id);
        Task<IEnumerable<ResponseSalariesDTO>> GetAllAsync();
        Task<ResponseSalariesDTO?> GetByIdAsync(int id);
        Task<int> UpdateAsync(int id, RequestSalariesDto requestSalaryDto);
    }
}

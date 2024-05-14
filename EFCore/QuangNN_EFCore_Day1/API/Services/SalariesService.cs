using API.DTOs.RequestDTOs;
using API.DTOs.ResponseDTOs;
using AutoMapper;
using Infrastructure.GenericRepository;
using Infrastructure.Models;

namespace API.Services
{
    public class SalariesService : ISalariesService
    {
        private readonly IGenericRepository<Salaries> _salaryRepository;
        private readonly IMapper _mapper;

        public SalariesService(IGenericRepository<Salaries> salaryRepository, IMapper mapper)
        {
            _salaryRepository = salaryRepository;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(RequestSalariesDto requestSalaryDto)
        {
            var salary = _mapper.Map<Salaries>(requestSalaryDto);
            return await _salaryRepository.AddAsync(salary);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _salaryRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ResponseSalariesDTO>> GetAllAsync()
        {
            var salaries = await _salaryRepository.GetAllAsync();
            var salaryDtos = _mapper.Map<IEnumerable<ResponseSalariesDTO>>(salaries);
            return salaryDtos;
        }

        public async Task<ResponseSalariesDTO?> GetByIdAsync(int id)
        {
            var salary = await _salaryRepository.GetByIdAsync(id);
            var salaryDto = _mapper.Map<ResponseSalariesDTO>(salary);
            return salaryDto;
        }

        public async Task<int> UpdateAsync(int id, RequestSalariesDto requestSalaryDto)
        {
            var salary = _mapper.Map<Salaries>(requestSalaryDto);
            salary.Id = id;
            return await _salaryRepository.UpdateAsync(salary);
        }
    }
}

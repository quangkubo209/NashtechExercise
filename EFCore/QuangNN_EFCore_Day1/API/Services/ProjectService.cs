using API.DTOs.RequestDTOs;
using API.DTOs.ResponseDTOs;
using AutoMapper;
using Infrastructure.GenericRepository;
using Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IGenericRepository<Project> _projectRepository;
        private readonly IGenericRepository<Project_Employee> _projectEmployeeRepository;
        private readonly IMapper _mapper;

        public ProjectService(IGenericRepository<Project> projectRepository,
                              IGenericRepository<Project_Employee> projectEmployeeRepository,
                              IMapper mapper)
        {
            _projectRepository = projectRepository;
            _projectEmployeeRepository = projectEmployeeRepository;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(RequestProjectDTO requestProjectDto)
        {
            var project = _mapper.Map<Project>(requestProjectDto);
            return await _projectRepository.AddAsync(project);
        }

        public async Task<int> DeleteAsync(int id)
        {
            // Xóa bản ghi dự án của dự án trước khi xóa dự án
            var projectEmployees = await _projectEmployeeRepository.GetAllAsync();
            foreach (var projectEmployee in projectEmployees)
            {
                await _projectEmployeeRepository.DeleteAsync(id);
            }

            return await _projectRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ResponseProjectDTO>> GetAllAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            var projectDtos = _mapper.Map<IEnumerable<ResponseProjectDTO>>(projects);
            return projectDtos;
        }

        public async Task<ResponseProjectDTO?> GetByIdAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            var projectDto = _mapper.Map<ResponseProjectDTO>(project);
            return projectDto;
        }

        public async Task<int> UpdateAsync(int id, RequestProjectDTO requestProjectDto)
        {
            var project = _mapper.Map<Project>(requestProjectDto);
            project.Id = id;
            return await _projectRepository.UpdateAsync(project);
        }
    }
}

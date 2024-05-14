
using API.DTOs.RequestDTOs;
using API.DTOs.ResponseDTOs;
using AutoMapper;
using Infrastructure.Models;

namespace QuangNN_ASPWebAPI2.API.DTOs
{
    public class AutoMapperRokies : Profile
    {
        public AutoMapperRokies()
        {
            CreateMap<RequestDepartmentDTO, Department>();
            CreateMap<Department, ResponseDepartmentDTO>();
            CreateMap<RequestEmployeeDTO,  Employee>();
            CreateMap<Employee, ResponseEmployeeDTO>();
            CreateMap<RequestSalariesDto, Salaries>();
            CreateMap<Salaries, ResponseSalariesDTO>();
            CreateMap<RequestProjectDTO, Project>();
            CreateMap<Project, ResponseProjectDTO>();
        }
    }
}


using API.DTOs.RequestDTOs;
using API.DTOs.ResponseDTOs;
using AutoMapper;
using QuangNN_ASPWebAPI2.Infrastructure.Models;

namespace QuangNN_ASPWebAPI2.API.DTOs
{
    public class AutoMapperPerson : Profile
    {
        public AutoMapperPerson()
        {
            CreateMap<RequestPersonDTO, Person>();
            CreateMap<Person, ResponsePersonDTO>();
        }
    }
}

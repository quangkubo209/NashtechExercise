using AutoMapper;
using QuangNN_ASPWebAPI.Models;

namespace QuangNN_ASPWebAPI.DTOs
{
    public class AutoMapperTask : Profile
    {
        public AutoMapperTask()
        {
            CreateMap<TodoTaskDTO, TodoTask>();
        }
    }
}

using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ASPNet_Day1.Models.DTOs
{
    public class AutoMapperPerson : Profile
    {
        public AutoMapperPerson()
        {
            CreateMap<PersonViewModel, Person>();
            CreateMap<Person, PersonViewModel>();
        }
    }
}

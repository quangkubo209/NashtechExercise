using API.DTOs.RequestDTOs;
using API.DTOs.ResponseDTOs;
using Common;
using Microsoft.AspNetCore.Mvc;
using QuangNN_ASPWebAPI2.Infrastructure.Models;

namespace API.Services
{
    public interface IPersonService
    {
        public IEnumerable<ResponsePersonDTO> GetPersons();
        public Person Create(RequestPersonDTO newPerson);

        public int UpdateById(Guid Id, RequestPersonDTO requestDTO);

        public ResponsePersonDTO GetById(Guid Id);

        public int DeleteById(Guid Id);

        public IEnumerable<ResponsePersonDTO> GetFilterPerson(RequestFilterDTO requestFilterDto);



    }
}

using API.DTOs.RequestDTOs;
using Common;
using QuangNN_ASPWebAPI2.Infrastructure.Models;

namespace API.Services.Validations
{
    public interface IValidationPerson
    {
        public ValidationResult Validate(RequestPersonDTO person);
    }
}

using API.DTOs.RequestDTOs;
using Common;
using QuangNN_ASPWebAPI2.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace API.Services.Validations
{
    public class ValidationPerson : IValidationPerson
    {
        public Common.ValidationResult Validate(RequestPersonDTO person)
        {
            var validationResult = new Common.ValidationResult();

            if (person == null)
            {
                validationResult.Errors[nameof(RequestPersonDTO)] = ValidationMessage.ObjectIsNull;
                return validationResult;
            }

            if (string.IsNullOrEmpty(person.FirstName))
            {
                validationResult.Errors[nameof(RequestPersonDTO.FirstName)] = ValidationMessage.ObjectIsNull;
            }

            if (string.IsNullOrEmpty(person.LastName))
            {
                validationResult.Errors[nameof(RequestPersonDTO.LastName)] = ValidationMessage.ObjectIsNull;
            }

            if (!string.IsNullOrEmpty(person.DateOfBirth))
            {
                string dateFormat = "dd-MM-yyyy";
                if (!IsValidDateFormat(person.DateOfBirth, dateFormat))
                {
                    validationResult.Errors[nameof(RequestPersonDTO.DateOfBirth)] = $"Invalid date format. The format should be '{dateFormat}'";
                }
            }



            validationResult.IsValid = validationResult.Errors.Count == 0; 

            return validationResult;
        }

        private bool IsValidDateFormat(string dateOfBirthString, string dateFormat)
        {
            DateTime parsedDate;
            return DateTime.TryParseExact(dateOfBirthString, dateFormat, null, System.Globalization.DateTimeStyles.None, out parsedDate);
        }
    }
    
}

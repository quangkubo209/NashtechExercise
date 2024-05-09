using QuangNN_ASPWebAPI2.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.RequestDTOs
{
    public class RequestPersonDTO
    {
        [Required(ErrorMessage ="Firstname is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        public string LastName { get; set; }
        public GenderEnum Gender { get; set; }

        [Required(ErrorMessage = "DateOfBirth is required")]
        public string DateOfBirth { get; set; }

        public string? BirthPlace { get; set; }
    }
}

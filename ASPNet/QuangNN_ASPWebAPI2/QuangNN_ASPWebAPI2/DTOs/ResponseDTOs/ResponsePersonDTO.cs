using QuangNN_ASPWebAPI2.Infrastructure;

namespace API.DTOs.ResponseDTOs
{
    public class ResponsePersonDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderEnum Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string BirthPlace { get; set; }
    }
}

using QuangNN_ASPWebAPI2.Infrastructure;

namespace API.DTOs.RequestDTOs
{
    public class RequestFilterDTO
    {
        public string Name { get; set; }
        public GenderEnum Gender { get; set; }
        public string BirthPlace { get; set; }
    }
}

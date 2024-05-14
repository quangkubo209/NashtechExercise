using Infrastructure.Models;

namespace API.DTOs.ResponseDTOs
{
    public class ResponseSalariesDTO
    {
        public int Id { get; set; }
        public int Salary { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}

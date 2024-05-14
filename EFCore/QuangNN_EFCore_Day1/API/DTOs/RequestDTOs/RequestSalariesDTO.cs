using System.ComponentModel.DataAnnotations;

namespace API.DTOs.RequestDTOs
{
    public class RequestSalariesDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Salary must be greater than 0")]
        public int Salary { get; set; }
        public int EmployeeId { get; set; }
    }
}

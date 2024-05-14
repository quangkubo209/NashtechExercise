using System.ComponentModel.DataAnnotations;

namespace API.DTOs.RequestDTOs
{
    public class RequestEmployeeDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime JoinedDate { get; set; }
        public int? DepartmentId { get; set; }
    }
}

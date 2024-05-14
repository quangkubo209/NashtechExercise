using System.ComponentModel.DataAnnotations;

namespace API.DTOs.RequestDTOs
{
    public class RequestDepartmentDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

    }
}

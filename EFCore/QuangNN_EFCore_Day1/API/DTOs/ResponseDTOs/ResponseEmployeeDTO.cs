using System.ComponentModel.DataAnnotations;

namespace API.DTOs.ResponseDTOs
{
    public class ResponseEmployeeDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime JoinedDate { get; set; }
        public decimal? Salary { get; set; }
        public ResponseDepartmentDTO? Department { get; set; }
        public ICollection<ResponseProjectDTO> Projects { get; set; }
    }
}

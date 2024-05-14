namespace API.DTOs.RequestDTOs
{
    public class RequestProjectEmployeeDTO
    {
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
        public bool Enable { get; set; }
    }
}

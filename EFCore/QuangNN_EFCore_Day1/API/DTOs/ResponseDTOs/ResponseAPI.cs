namespace API.DTOs.ResponseDTOs
{
    public class ResponseAPI
    {
        public object Data { get; set; } = new { };

        public int StatusCode { get; set; } = StatusCodes.Status200OK;

        public string Message { get; set; } = string.Empty;
    }
}

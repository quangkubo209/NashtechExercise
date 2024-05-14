using API.DTOs.RequestDTOs;
using API.DTOs.ResponseDTOs;
using API.Services;
using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("salaries")]
    [ApiController]
    public class SalariesController : ControllerBase
    {
        private readonly ISalariesService _salaryService;

        public SalariesController(ISalariesService salaryService)
        {
            _salaryService = salaryService;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<ResponseSalariesDTO>>>> GetSalaries()
        {
            var salaries = await _salaryService.GetAllAsync();
            if (!salaries.Any())
            {
                return new SuccessGeneralResponse<IEnumerable<ResponseSalariesDTO>>
                {
                    Message = "List is empty"
                };
            }
            else
            {
                return new SuccessGeneralResponse<IEnumerable<ResponseSalariesDTO>>
                {
                    Content = salaries,
                    Message = "Get list successfully"
                };
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<ResponseSalariesDTO>>> GetSalary(int id)
        {
            var salary = await _salaryService.GetByIdAsync(id);
            if (salary == null)
            {
                return new GeneralResponse<ResponseSalariesDTO>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Salary not found"
                };
            }
            else
            {
                return new SuccessGeneralResponse<ResponseSalariesDTO>
                {
                    Content = salary,
                    Message = "Get salary successfully"
                };
            }
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse>> Add([FromBody] RequestSalariesDto requestSalaryDto)
        {
            var addStatus = await _salaryService.AddAsync(requestSalaryDto);
            if (addStatus == ConstantsStatus.Success)
            {
                return new SuccessGeneralResponse
                {
                    Message = "Create new salary successfully!"
                };
            }
            else
            {
                return new GeneralResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Create new salary failed!"
                };
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse>> Delete(int id)
        {
            var deleteStatus = await _salaryService.DeleteAsync(id);
            if (deleteStatus == ConstantsStatus.Success)
            {
                return new SuccessGeneralResponse
                {
                    Message = "Delete salary successfully!"
                };
            }
            else
            {
                return new GeneralResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Delete salary failed!"
                };
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse>> Update(int id, [FromBody] RequestSalariesDto requestSalaryDto)
        {
            var updateStatus = await _salaryService.UpdateAsync(id, requestSalaryDto);
            if (updateStatus == ConstantsStatus.Success)
            {
                return new SuccessGeneralResponse
                {
                    Message = "Update salary successfully!"
                };
            }
            else
            {
                return new GeneralResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Update salary failed!"
                };
            }
        }
    }
}

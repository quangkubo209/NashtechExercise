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
    [Route("departments")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<ResponseDepartmentDTO>>>> GetDepartments()
        {
            var departments = await _departmentService.GetAllAsync();
            if (departments.Count() == 0)
            {
                return new SuccessGeneralResponse<IEnumerable<ResponseDepartmentDTO>>
                {
                    Message = "List is empty"
                };
            }
            else
            {
                return new SuccessGeneralResponse<IEnumerable<ResponseDepartmentDTO>>
                {
                    Content = departments,
                    Message = "Get list successfully"
                };
            }
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse>> Add([FromBody] RequestDepartmentDTO requestDepartmentDto)
        {
            var addStatus = await _departmentService.AddAsync(requestDepartmentDto);
            if (addStatus == ConstantsStatus.Success)
            {
                return new SuccessGeneralResponse
                {
                    Message = "Create new department successfully!"
                };
            }
            else
            {
                return new GeneralResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Create new department failed!"
                };
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse>> Delete(int id)
        {
            var deleteStatus = await _departmentService.DeleteAsync(id);
            if (deleteStatus == ConstantsStatus.Success)
            {
                return new SuccessGeneralResponse
                {
                    Message = "Delete department successfully!"
                };
            }
            else
            {
                return new GeneralResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Delete department failed!"
                };
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse>> Update(int id, [FromBody] RequestDepartmentDTO requestDepartmentDto)
        {
            var updateStatus = await _departmentService.UpdateAsync(id, requestDepartmentDto);
            if (updateStatus == ConstantsStatus.Success)
            {
                return new SuccessGeneralResponse
                {
                    Message = "Update department successfully!"
                };
            }
            else
            {
                return new GeneralResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Update department failed!"
                };
            }
        }
    }
}

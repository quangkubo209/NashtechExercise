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
    [Route("employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<ResponseEmployeeDTO>>>> GetEmployees()
        {
            var employees = await _employeeService.GetAllAsync();
            if (!employees.Any())
            {
                return new SuccessGeneralResponse<IEnumerable<ResponseEmployeeDTO>>
                {
                    Message = "List is empty"
                };
            }
            else
            {
                return new SuccessGeneralResponse<IEnumerable<ResponseEmployeeDTO>>
                {
                    Content = employees,
                    Message = "Get list successfully"
                };
            }
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse>> Add([FromBody] RequestEmployeeDTO requestEmployeeDto)
        {
            var addStatus = await _employeeService.AddAsync(requestEmployeeDto);
            if (addStatus == ConstantsStatus.Success)
            {
                return new SuccessGeneralResponse
                {
                    Message = "Create new employee successfully!"
                };
            }
            else
            {
                return new GeneralResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Create new employee failed!"
                };
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse>> Delete(int id)
        {
            var deleteStatus = await _employeeService.DeleteAsync(id);
            if (deleteStatus == ConstantsStatus.Success)
            {
                return new SuccessGeneralResponse
                {
                    Message = "Delete employee successfully!"
                };
            }
            else
            {
                return new GeneralResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Delete employee failed!"
                };
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse>> Update(int id, [FromBody] RequestEmployeeDTO requestEmployeeDto)
        {
            var updateStatus = await _employeeService.UpdateAsync(id, requestEmployeeDto);
            if (updateStatus == ConstantsStatus.Success)
            {
                return new SuccessGeneralResponse
                {
                    Message = "Update employee successfully!"
                };
            }
            else
            {
                return new GeneralResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Update employee failed!"
                };
            }
        }


        [HttpGet]
        [Route("employee-department")]
        public async Task<ActionResult<GeneralResponse<IEnumerable<ResponseEmployeeDepartmentDTO>>>> GetAllEmployeesWithDepartments()
        {
            var employees = await _employeeService.GetAllEmployeesWithDepartments();
            if (!employees.Any())
            {
                return new SuccessGeneralResponse<IEnumerable<ResponseEmployeeDepartmentDTO>>
                {
                    Message = "List is empty"
                };
            }
            else
            {
                return new SuccessGeneralResponse<IEnumerable<ResponseEmployeeDepartmentDTO>>
                {
                    Content = employees,
                    Message = "Get list employee with department successfully!"
                };
            }
        }

        [HttpGet]
        [Route("employee-project")]
        public async Task<ActionResult<GeneralResponse<IEnumerable<ResponseEmployeeProjectDTO>>>> GetAllEmployeeWithProjects()
        {
            var employees = await _employeeService.GetAllEmployeesWithProjects();
            if (!employees.Any())
            {
                return new SuccessGeneralResponse<IEnumerable<ResponseEmployeeProjectDTO>>
                {
                    Message = "List is empty"
                };
            }
            else
            {
                return new SuccessGeneralResponse<IEnumerable<ResponseEmployeeProjectDTO>>
                {
                    Content = employees,
                    Message = "Get list employee with projects successfully!"
                };
            }
        }

        [HttpGet]
        [Route("employee-filter")]
        public async Task<ActionResult<GeneralResponse<IEnumerable<ResponseEmployeeDTO>>>> GetEmployeeWithFilter([FromBody] int minSalary, string minJoinedDate)
        {
            var employees = await _employeeService.GetEmployeeWithFilter(minSalary, minJoinedDate);
            if (!employees.Any())
            {
                return new SuccessGeneralResponse<IEnumerable<ResponseEmployeeDTO>>
                {
                    Message = "List is empty"
                };
            }
            else
            {
                return new SuccessGeneralResponse<IEnumerable<ResponseEmployeeDTO>>
                {
                    Content = employees,
                    Message = "Get list employee  successfully!"
                };
            }
        }



    }
}

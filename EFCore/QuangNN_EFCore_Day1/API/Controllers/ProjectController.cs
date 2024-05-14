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
    [Route("projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<ResponseProjectDTO>>>> GetProjects()
        {
            var projects = await _projectService.GetAllAsync();
            if (!projects.Any())
            {
                return new SuccessGeneralResponse<IEnumerable<ResponseProjectDTO>>
                {
                    Message = "List is empty"
                };
            }
            else
            {
                return new SuccessGeneralResponse<IEnumerable<ResponseProjectDTO>>
                {
                    Content = projects,
                    Message = "Get list successfully"
                };
            }
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse>> Add([FromBody] RequestProjectDTO requestProjectDto)
        {
            var addStatus = await _projectService.AddAsync(requestProjectDto);
            if (addStatus == ConstantsStatus.Success)
            {
                return new SuccessGeneralResponse
                {
                    Message = "Create new project successfully!"
                };
            }
            else
            {
                return new GeneralResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Create new project failed!"
                };
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse>> Delete(int id)
        {
            var deleteStatus = await _projectService.DeleteAsync(id);
            if (deleteStatus == ConstantsStatus.Success)
            {
                return new SuccessGeneralResponse
                {
                    Message = "Delete project successfully!"
                };
            }
            else
            {
                return new GeneralResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Delete project failed!"
                };
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse>> Update(int id, [FromBody] RequestProjectDTO requestProjectDto)
        {
            var updateStatus = await _projectService.UpdateAsync(id, requestProjectDto);
            if (updateStatus == ConstantsStatus.Success)
            {
                return new SuccessGeneralResponse
                {
                    Message = "Update project successfully!"
                };
            }
            else
            {
                return new GeneralResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Update project failed!"
                };
            }
        }
    }
}

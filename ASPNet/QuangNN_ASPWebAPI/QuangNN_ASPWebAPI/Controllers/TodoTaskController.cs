using Microsoft.AspNetCore.Mvc;
using QuangNN_ASPWebAPI.Common;
using QuangNN_ASPWebAPI.DTOs;
using QuangNN_ASPWebAPI.Models;
using QuangNN_ASPWebAPI.Services;
using System.Net;

namespace QuangNN_ASPWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoTaskController : ControllerBase
    {
        private readonly ILogger<TodoTaskController> _logger;
        private readonly ITodoTaskService _todoTaskService;

        public TodoTaskController(ILogger<TodoTaskController> logger, ITodoTaskService todoTaskService)
        {
            _logger = logger;
            _todoTaskService = todoTaskService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoTask>> GetList()
        {
            var listTodoTasks =  _todoTaskService.GetTasks();
            return Ok(listTodoTasks);
        }

        [HttpGet("{id}")]
        public ActionResult<TodoTaskDTO> GetById(Guid id)
        {
            
                var todoTask = _todoTaskService.GetById(id);
                if (todoTask == null)
                {
                    return NotFound();
                }
                return Ok(todoTask);
            
            return null;
        }

        [HttpPost]
        public ActionResult<TodoTaskDTO> Create([FromBody] TodoTaskDTO dto)
        {
            if (dto == null)
            {
                return NotFound();
            }
            var newItem = _todoTaskService.Create(dto);
            return CreatedAtAction(nameof(GetList), newItem);
        }

        [HttpPut]
        public ActionResult Update(Guid Id,[FromBody] TodoTaskDTO dto)
        {
            _todoTaskService.UpdateById(Id, dto);
            return Ok("Update successfully");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            _todoTaskService.DeleteById(id);
            return Ok("Delete todoTask with Id " + id+" successfull!");
        }

        [HttpPost("/bulk")]
        public IActionResult BulkAdd(List<TodoTaskDTO> taskDtos)
        {
            int status = _todoTaskService.BulkAdd(taskDtos);
            if (status == ConstantsStatus.Success)
            {
                return Ok("Create list new tasks successfully!");
            }
            else
            {
                return BadRequest("Create list new tasks failed!");
            }
        }

        [HttpDelete("/bulk")]
        public IActionResult BulkDelete(List<Guid> ids)
        {
            int status = _todoTaskService.BulkDelete(ids);
            if (status == ConstantsStatus.Success)
            {
                return Ok("Delete list tasks successfully!");
            }
            else
            {
                return BadRequest("Delete list tasks failed!");
            }
        }




    }
}

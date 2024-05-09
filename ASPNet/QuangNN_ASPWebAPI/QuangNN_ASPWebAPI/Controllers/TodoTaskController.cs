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
            int status = _todoTaskService.UpdateById(Id, dto);
            if(status == ConstantsStatus.Success)
            {
                return Ok("Update task with id: " + Id + " successfully");
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            int status = _todoTaskService.DeleteById(id);
            if (status == ConstantsStatus.Success)
            {
                return Ok("Delete task with id: " + id + " successfully");
            }
            return BadRequest();
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

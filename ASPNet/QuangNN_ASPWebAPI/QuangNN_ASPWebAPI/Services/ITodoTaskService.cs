using QuangNN_ASPWebAPI.DTOs;
using QuangNN_ASPWebAPI.Models;

namespace QuangNN_ASPWebAPI.Services
{
    public interface ITodoTaskService
    {
        public IEnumerable<TodoTask> GetTasks();
        public TodoTask Create(TodoTaskDTO newTodoTask);

        public void UpdateById(Guid Id, TodoTaskDTO todoTaskDto);

        public TodoTask GetById(Guid Id);

        public void DeleteById(Guid Id);

        int BulkDelete(List<Guid> ids);
        int BulkAdd(List<TodoTaskDTO> tasks);


    }
}

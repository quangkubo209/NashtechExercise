using QuangNN_ASPWebAPI.DTOs;
using QuangNN_ASPWebAPI.Models;
using System;

namespace QuangNN_ASPWebAPI.Repositories
{
    public interface ITodoTaskRepository
    {
        public List<TodoTask> GetTasks();
        public int BulkDelete(List<Guid> ids);
        public int BulkAdd(List<TodoTask> tasks);
    }
}

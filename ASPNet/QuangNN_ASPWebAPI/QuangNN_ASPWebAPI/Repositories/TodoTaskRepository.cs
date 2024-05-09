using QuangNN_ASPWebAPI.Common;
using QuangNN_ASPWebAPI.DTOs;
using QuangNN_ASPWebAPI.Models;
using System.Net;
using System.Threading.Tasks;

namespace QuangNN_ASPWebAPI.Repositories
{
    public class TodoTaskRepository : ITodoTaskRepository
    {
        List<TodoTask> _todoTask = new List<TodoTask> { };
        public List<TodoTask> GetTasks()
        {
            return _todoTask;
        }

        public int BulkDelete(List<Guid> ids)
        {
            var taskList = _todoTask.ToList();
            int numberOfRecordsRemoved = taskList.RemoveAll(task => ids.Contains(task.Id));
            if (numberOfRecordsRemoved == 0)
                return ConstantsStatus.Failed;
            else
            {
                _todoTask = taskList;
                return ConstantsStatus.Success;
            }
        }
        public int BulkAdd(List<TodoTask> tasks)
        {
            var taskList = _todoTask.ToList();
            taskList.AddRange(tasks);
            if (taskList.Count > _todoTask.Count())
            {
                _todoTask = taskList;
                return ConstantsStatus.Success;
            }
            else
                return ConstantsStatus.Failed;
        }

        public int UpdateById(Guid Id, TodoTask todoTask)
        {
            var existingTodoTask = _todoTask.FirstOrDefault(p => p.Id == Id);
            if (existingTodoTask != null)
            {
                existingTodoTask.Title = todoTask.Title;
                existingTodoTask.IsCompleted = todoTask.IsCompleted;
                return ConstantsStatus.Success;
            }
            return ConstantsStatus.Failed;
        }

        public int DeleteById(Guid id)
        {
            var deleteTask = _todoTask.FirstOrDefault(task => task.Id == id);
            if (deleteTask != null)
            {
                var taskList = _todoTask.ToList();
                if (taskList.Remove(deleteTask))
                {
                    _todoTask = taskList;
                    return ConstantsStatus.Success;
                }
                return ConstantsStatus.Failed;
            }
            return ConstantsStatus.Failed;
        }

        public TodoTask GetById(Guid Id)
        {
            var task = _todoTask.FirstOrDefault(t => t.Id == Id);
            return task;
        }
    }
}

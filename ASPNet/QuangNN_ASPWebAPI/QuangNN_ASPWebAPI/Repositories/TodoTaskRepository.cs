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
    }
}

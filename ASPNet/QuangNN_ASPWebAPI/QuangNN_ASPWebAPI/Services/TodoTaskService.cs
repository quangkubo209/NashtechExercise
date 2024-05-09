using AutoMapper;
using QuangNN_ASPWebAPI.DTOs;
using QuangNN_ASPWebAPI.Models;
using QuangNN_ASPWebAPI.Repositories;

namespace QuangNN_ASPWebAPI.Services
{
    public class TodoTaskService: ITodoTaskService
    {
        private readonly ITodoTaskRepository _todoTaskRepository;
        private List<TodoTask> _tasks;
        private readonly IMapper _mapper;


        public TodoTaskService(ITodoTaskRepository todoTaskRepository, IMapper mapper)
        {

            _todoTaskRepository = todoTaskRepository;
            _tasks = _todoTaskRepository.GetTasks();
            _mapper = mapper;
        }

        public IEnumerable<TodoTask> GetTasks()
        {
            return _todoTaskRepository.GetTasks();
        }

        public TodoTask Create(TodoTaskDTO todoTaskDTO)
        {
            if (todoTaskDTO != null)
            {
                var newTask = _mapper.Map<TodoTask>(todoTaskDTO);
                newTask.Id = Guid.NewGuid();


                _tasks.Add(newTask);
                return newTask;
            }
            return null;
        }

        public void UpdateById(Guid Id ,TodoTaskDTO todoTaskDTO)
        {
            var existingTodoTask = _tasks.FirstOrDefault(p => p.Id == Id);
            if (existingTodoTask != null)
            {
                existingTodoTask.Title = todoTaskDTO.Title;
                existingTodoTask.IsCompleted = todoTaskDTO.IsCompleted;
            }
        }

        public void DeleteById(Guid id)
        {
            var taskToDelete = _tasks.FirstOrDefault(p => p.Id == id);
            if (taskToDelete != null)
            {
                _tasks.Remove(taskToDelete);
            }
        }

        public TodoTask GetById(Guid Id)
        {
            return _tasks.FirstOrDefault(p => p.Id == Id);
        }

        public int BulkAdd(List<TodoTaskDTO> todoTaskDtos)
        {
            var tasks = _mapper.Map<List<TodoTask>>(todoTaskDtos);
            tasks.ForEach(task => task.Id = Guid.NewGuid());
            return _todoTaskRepository.BulkAdd(tasks);
        }

        public int BulkDelete(List<Guid> ids)
        {
            return _todoTaskRepository.BulkDelete(ids);
        }



    }
}

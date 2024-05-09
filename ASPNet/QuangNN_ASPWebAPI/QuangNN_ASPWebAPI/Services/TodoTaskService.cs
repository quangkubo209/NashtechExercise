using AutoMapper;
using QuangNN_ASPWebAPI.DTOs;
using QuangNN_ASPWebAPI.Models;
using QuangNN_ASPWebAPI.Repositories;
using System.Reflection.Metadata;

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

        public int UpdateById(Guid id ,TodoTaskDTO todoTaskDTO)
        {
                var todoTask = _mapper.Map<TodoTask>(todoTaskDTO);
                return _todoTaskRepository.UpdateById(id, todoTask);
        }

        public int DeleteById(Guid id)
        {
            return _todoTaskRepository.DeleteById(id);
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

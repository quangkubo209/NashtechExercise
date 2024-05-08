using System;

namespace QuangNN_ASPWebAPI.Models
{
    public class TodoTask
    {
        public TodoTask() { }

        public TodoTask(Guid Id, string Title, bool IsCompleted)
        {
            this.Id = Id;
            this.Title = Title;
            this.IsCompleted = IsCompleted;
        }
        public Guid Id { get; set; }

        public string Title { get; set; }
        
        public bool IsCompleted { get; set; }
    }
}

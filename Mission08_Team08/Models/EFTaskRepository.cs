﻿
namespace Mission08_Team08.Models
{
    public class EFTaskRepository : TaskRepository
    {
        private TaskDBContext _context;

        public EFTaskRepository(TaskDBContext temp)
        {
            _context = temp;
        }

        public List<Task> Tasks => _context.Tasks.ToList();

        public void AddTask(Task task)
        {
            _context.Add(task);
            _context.SaveChanges();
        }
    }
}

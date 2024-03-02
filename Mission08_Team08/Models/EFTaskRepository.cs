using Microsoft.EntityFrameworkCore;

namespace Mission08_Team08.Models
{
    public class EFTaskRepository : TaskRepository
    {
        private TaskDBContext _context;

        public EFTaskRepository(TaskDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Task> GetTasksWithCategories()
        {
            return _context.Tasks
                           .Include(t => t.Category) // Eagerly load the Category with each task
                           .ToList();
        }

        public List<Task> Tasks => _context.Tasks.ToList();

        public IEnumerable<Category> Categories => _context.Categories.ToList(); // Implemented line

        public void AddTask(Task task)
        {
            _context.Add(task);
            _context.SaveChanges();
        }

        public void RemoveTask(Task task)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }

        public void UpdateTask(Task task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }
    }
}

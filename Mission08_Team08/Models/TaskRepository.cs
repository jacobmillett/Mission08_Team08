namespace Mission08_Team08.Models
{
    public interface TaskRepository
    {
        List<Task> Tasks { get; }
        IEnumerable<Category> Categories { get; } // Added

        IEnumerable<Task> GetTasksWithCategories();

        void AddTask(Task task);
        void RemoveTask(Task task);
        void UpdateTask(Task task);
    }
}

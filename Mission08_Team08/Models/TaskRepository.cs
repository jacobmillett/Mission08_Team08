namespace Mission08_Team08.Models
{
    public interface TaskRepository
    {
        List <Task> Tasks { get; }

        public void AddTask(Task task);

        public void RemoveTask(Task task);

        public void UpdateTask(Task task);
    }
}

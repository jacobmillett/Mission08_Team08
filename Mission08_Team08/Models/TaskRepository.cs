namespace Mission08_Team08.Models
{
    public interface TaskRepository
    {
        List <Task> Tasks { get; }

        public void AddTask(Task task);
    }
}

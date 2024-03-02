using Microsoft.AspNetCore.Mvc;

namespace Mission08_Team08.Models
{
    public class AddNewTask
    {
        public Quadrant Quadrant { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string DueDate { get; set; } // Assuming input as string; convert as needed
        public int? CategoryId { get; set; } // Use this for the foreign key relationship
        public bool Completed { get; set; }
    }
}



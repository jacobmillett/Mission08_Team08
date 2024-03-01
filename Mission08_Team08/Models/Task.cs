// Author: Reed Stewart Desc: Main model

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mission08_Team08.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }
        [Required(ErrorMessage = "Please input a task name")]
        public string TaskName { get; set; }
        public string? DueDate { get; set; }
        [Required(ErrorMessage = "Please input a quadrant")]
        public int Quadrant { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public bool Completed { get; set; }

    }
}

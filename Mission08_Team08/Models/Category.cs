using System.ComponentModel.DataAnnotations;

namespace Mission08_Team08.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}

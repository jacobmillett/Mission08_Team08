using Microsoft.AspNetCore.Mvc;

namespace Mission08_Team08.Models
{
    public enum Quadrant
    {
        ImportantUrgent = 1, // Quadrant I
        ImportantNotUrgent = 2, // Quadrant II
        NotImportantUrgent = 3, // Quadrant III
        NotImportantNotUrgent = 4 // Quadrant IV
    }
}


using Microsoft.AspNetCore.Mvc;
using Mission08_Team08.Models;
using System.Diagnostics;

namespace Mission08_Team08.Controllers
{
    public class HomeController : Controller
    {
        private TaskRepository _repo;

        public HomeController(TaskRepository temp)
        {
            _repo = temp;
        }

        public IActionResult Index()
        {
            return View();
        }

        
    }
}

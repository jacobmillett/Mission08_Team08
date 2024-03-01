using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission08_Team08.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        [HttpGet]
        public IActionResult AddNewTask()
        {
            //! Depends on the view and how you want to do this part !
            //ViewBag.Categories = _repo.Categories
            // .OrderBy(x => x.CategoryName)
            // .ToList();
            return View();
        }
        // Post side of the Enter a new movie controller
        [HttpPost]
        public IActionResult AddNewTask(AddNewTask response)
        {

            _repo.AddTask(response);
            
            return RedirectToAction("TaskList"); // Redirect to the movie list page after successful addition
        }
        
        public IActionResult TaskList()
        {
            var Tasks = _repo.Tasks
                .OrderBy(x => x.TaskName).ToList();

            return View(Tasks);
        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            var recordToEdit = _repo.Tasks
               .Single(x => x.TaskId == id);

            //ViewBag.Categories = _repo.Category
            //    .OrderBy(x => x.CategoryName)
            //    .ToList(); 
            //! This part will depend on the view !
            //! This line below is also going to depend on the name of the view !

            return View("AddNewTask", recordToEdit);
        }

        [HttpPost]

        public IActionResult Edit(AddNewTask task)
        {
            _repo.UpdateTask(task);

            return RedirectToAction("TaskList");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var recordToDelete = _repo.Tasks
                .Single(x => x.TaskId == id);
            return View(recordToDelete);
        }

        [HttpPost]
        
        public IActionResult Delete(Models.Task task)
        {
            _repo.RemoveTask(task);

            return RedirectToAction("TaskList");
        }

    }
}

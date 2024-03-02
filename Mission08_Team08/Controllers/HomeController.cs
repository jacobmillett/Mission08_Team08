using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mission08_Team08.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Task = Mission08_Team08.Models.Task;

namespace Mission08_Team08.Controllers
{
    public class HomeController : Controller
    {
        private TaskRepository _repo;

        public HomeController(TaskRepository repo)
        {
            _repo = repo;
        }

        //Route for Index.cshtml
        public IActionResult Index()
        {
            return View();
        }

        //GET mostly for the drop down
        [HttpGet]
        public IActionResult AddNewTask()
        {
            ViewBag.Categories = new SelectList(_repo.Categories, "CategoryId", "CategoryName");
            return View(new AddNewTask());
        }

        // POST for adding the task to the DB
        [HttpPost]
        public IActionResult AddNewTask(AddNewTask viewModel)
        {
            if (ModelState.IsValid)
            {
                var task = new Mission08_Team08.Models.Task
                {
                    TaskName = viewModel.TaskName,
                    DueDate = viewModel.DueDate, // Leaving this as a string and not converting
                    Quadrant = viewModel.Quadrant, // Enumerate cause that's important I guess
                    CategoryId = viewModel.CategoryId, 
                    Completed = viewModel.Completed // No bool to int conversion
                };

                _repo.AddTask(task);
                return RedirectToAction("Quadrants");
            }

            // If the model state is not valid, return to the view with the current model to show validation errors.
            return View(viewModel);
        }


        [HttpGet]
        public IActionResult EditTask(int taskId)
        {
            var task = _repo.Tasks.FirstOrDefault(t => t.TaskId == taskId);
            if (task == null)
            {
                return NotFound();
            }

            var viewModel = new EditTask // Created EditTask.cs model
            {
                TaskName = task.TaskName,
                DueDate = task.DueDate,
                Quadrant = task.Quadrant,
                CategoryId = task.CategoryId,
                Completed = task.Completed
            };

            ViewBag.Categories = new SelectList(_repo.Categories, "CategoryId", "CategoryName", task.CategoryId);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditTask(EditTask viewModel) // Created EditTask.cs model
        {
            if (ModelState.IsValid)
            {
                var task = _repo.Tasks.FirstOrDefault(t => t.TaskId == viewModel.TaskId);
                if (task == null)
                {
                    return NotFound();
                }

                // Update properties
                task.TaskName = viewModel.TaskName;
                task.DueDate = viewModel.DueDate;
                task.Quadrant = viewModel.Quadrant;
                task.CategoryId = viewModel.CategoryId;
                task.Completed = viewModel.Completed;

                _repo.UpdateTask(task); // Implement this method if it doesn't exist

                return RedirectToAction("Quadrants"); // Redirect after successful update
            }

            ViewBag.Categories = new SelectList(_repo.Categories, "CategoryId", "CategoryName", viewModel.CategoryId);
            return View(viewModel);
        }



        public IActionResult Quadrants()
        {
            var tasksWithCategories = _repo.GetTasksWithCategories();

            if (tasksWithCategories == null || !tasksWithCategories.Any())
            {
                // Handle the case where no tasks are returned from the repository
                // For example, by passing an empty model or by displaying a message in the view
                return View(Enumerable.Empty<IGrouping<Quadrant, Task>>());
            }

            var tasksByQuadrant = tasksWithCategories
                .GroupBy(t => t.Quadrant)
                .OrderBy(g => g.Key)
                .ToList();

            return View(tasksByQuadrant);
        }

    }
}

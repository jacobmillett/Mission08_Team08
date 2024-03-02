using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mission08_Team08.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Task = Mission08_Team08.Models.Task;

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
            ViewBag.Categories = new SelectList(_repo.Categories, "CategoryId", "CategoryName");
            return View(new AddNewTask());
        }

        [HttpPost]
        public IActionResult AddNewTask(AddNewTask viewModel)
        {
            if (ModelState.IsValid)
            {
                var task = new Mission08_Team08.Models.Task
                {
                    TaskName = viewModel.TaskName,
                    DueDate = Convert.ToDateTime(viewModel.DueDate), // Convert from string to DateTime
                    Quadrant = (Quadrant)(int)viewModel.Quadrant,
                    CategoryId = viewModel.CategoryId, // Assuming CategoryId is correctly an int?
                    Completed = viewModel.Completed // Assuming EF handles bool to int conversion
                };

                _repo.AddTask(task);
                return RedirectToAction("Quadrants");
            }

            // If the model state is not valid, return to the view with the current model to show validation errors.
            return View(viewModel);
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
            var task = _repo.Tasks.FirstOrDefault(t => t.TaskId == id);
            if (task == null)
            {
                return NotFound();
            }
            var viewModel = new AddNewTask
            {
                // Map task properties to viewModel
            };
            ViewBag.Categories = new SelectList(_repo.Categories, "CategoryId", "CategoryName", viewModel.CategoryId);
            return View("AddNewTask", viewModel);
        }

        [HttpPost]
        public IActionResult Edit(AddNewTask viewModel)
        {
            if (ModelState.IsValid)
            {
                var taskToUpdate = _repo.Tasks.FirstOrDefault(t => t.TaskId == viewModel.TaskId); // Find the task by ID

                if (taskToUpdate != null)
                {
                    // Update properties
                    taskToUpdate.TaskName = viewModel.TaskName;
                    taskToUpdate.DueDate = Convert.ToDateTime(viewModel.DueDate); // Ensure this is a DateTime?
                    taskToUpdate.Quadrant = (Quadrant)(int)viewModel.Quadrant;
                    taskToUpdate.CategoryId = viewModel.CategoryId; // This should be CategoryId
                    taskToUpdate.Completed = viewModel.Completed;

                    _repo.UpdateTask(taskToUpdate);
                    return RedirectToAction("Quadrants");
                }

                else
                {
                    // Handle the case where the task doesn't exist
                }
            }

            // If the model state is not valid, or if the task doesn't exist, return to the view with the current model
            return View(viewModel);
        }


        [HttpPost]
        
        public IActionResult Delete(Models.Task task)
        {
            _repo.RemoveTask(task);

            return RedirectToAction("Quadrants");
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

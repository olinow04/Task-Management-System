using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_Management_System.Data;
using Task_Management_System.Models;

namespace Task_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var isAdmin = User.IsInRole("Admin");

                
                var userProjects = isAdmin
                    ? _context.Projects.Include(p => p.Tasks).ToList()
                    : _context.Projects
                        .Include(p => p.Tasks)
                        .Where(p => p.OwnerId == userId || p.ProjectMembers.Any(pm => pm.UserId == userId))
                        .ToList();

               
                var userTasks = isAdmin
                    ? _context.TaskItems.ToList()
                    : _context.TaskItems
                        .Where(t => t.AssignedToUserId == userId ||
                                   t.Project!.OwnerId == userId ||
                                   t.Project!.ProjectMembers.Any(pm => pm.UserId == userId))
                        .ToList();

                
                ViewBag.ActiveProjects = userProjects.Count(p => p.Status == ProjectStatus.Active);
                ViewBag.TodoTasks = userTasks.Count(t => t.Status == Models.TaskStatus.ToDo);
                ViewBag.InProgressTasks = userTasks.Count(t => t.Status == Models.TaskStatus.InProgress);
                ViewBag.CompletedTasks = userTasks.Count(t => t.Status == Models.TaskStatus.Done);
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
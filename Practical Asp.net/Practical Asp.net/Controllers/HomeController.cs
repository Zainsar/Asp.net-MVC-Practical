using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practical_Asp.net.Models;
using System.Diagnostics;

namespace Practical_Asp.net.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Search(string query, int pageIndex = 1, int pageSize = 3)
        {
            if (string.IsNullOrEmpty(query))
            {
                var allStudents = _context.Students
                    .Include(s => s.Enrollments)
                        .ThenInclude(e => e.Course) 
                    .OrderBy(s => s.StudentId); 

                var count = allStudents.Count();
                var items = allStudents.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                var model = new PaginatedList<Student>(items, count, pageIndex, pageSize);
                return View("Index", model);
            }

            var results = _context.Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                .Where(s => s.StudentName.Contains(query) ||
                            s.Enrollments.Any(e => e.Course.CourseName.Contains(query)))
                .OrderBy(s => s.StudentId);

            var countResults = results.Count();
            var itemsResults = results.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            var modelResults = new PaginatedList<Student>(itemsResults, countResults, pageIndex, pageSize);

            return View("Index", modelResults);
        }

        public IActionResult Index(int pageIndex = 1, int pageSize = 5)
        {
            var students = _context.Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                .OrderBy(s => s.StudentId);

            var count = students.Count();
            var items = students.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            var model = new PaginatedList<Student>(items, count, pageIndex, pageSize);

            return View(model);
        }


        public IActionResult Privacy()
        {
            return View();
        }
    }
}
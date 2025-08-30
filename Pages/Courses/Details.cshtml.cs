using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;

namespace StudentManagementRazorClientApp.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        private readonly CourseService _courseService;                                                      // Service for API calls

        public DetailsModel(CourseService courseService)                                                    // Constructor with dependency injection of CourseService
        {
            _courseService = courseService;                                                                 // Assign service
        }

        public CourseModel Course { get; set; } = new CourseModel(0, string.Empty, 0);                      // Property bound to the form inputs on the Details Page

        // GET handler: Fetch course by ID
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);                                       // Fetch course
            if (course == null) return NotFound();                                                          // Return 404 if not found

            Course = course;                                                                                // Bind to property
            return Page();                                                                                  // Render page
        }
    }
}
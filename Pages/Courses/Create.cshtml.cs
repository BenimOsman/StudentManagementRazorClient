using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;

namespace StudentManagementRazorClientApp.Pages.Courses
{
    public class CreateModel : PageModel
    {
        private readonly CourseService _courseService;                                                      // Service to interact with Course API

        public CreateModel(CourseService courseService)                                                     // Constructor with dependency injection of CourseService
        {
            _courseService = courseService;                                                                 // Assign injected service to local variable
        }

        [BindProperty]                                                                                      // Property bound to the form inputs on the Create page
        public CourseModel Course { get; set; } = new CourseModel(0, string.Empty, 0);                      // Initialize with default values for positional record

        // GET handler (loads the page initially)
        public void OnGet()                                                                                 // Nothing to do here, page loads with empty CourseModel
        {

        }

        // POST handler (called when form is submitted)
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)                                                                        // Validate form inputs
                return Page();                                                                              // Return page if validation fails

            var createdCourse = await _courseService.AddCourseAsync(Course);                                // Call the API to create the new course

            if (createdCourse == null)                                                                      // If API fails, show error message on page
            {
                ModelState.AddModelError(string.Empty, "Error creating course. Please try again.");
                return Page();
            }

            return RedirectToPage("Index");                                                                 // Redirect to Index page after successful creation
        }
    }
}
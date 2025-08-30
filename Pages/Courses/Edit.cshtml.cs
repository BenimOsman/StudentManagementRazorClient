using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;

namespace StudentManagementRazorClientApp.Pages.Courses
{
    public class EditModel : PageModel
    {
        private readonly CourseService _courseService;                                                      // Service to interact with Course API

        public EditModel(CourseService courseService)                                                       // Constructor with dependency injection of CourseService
        {
            _courseService = courseService;                                                                 // Assign injected service to local variable
        }

        [BindProperty]                                                                                      // Property bound to the form inputs on the Edit page
        public CourseModel Course { get; set; } = new CourseModel(0, string.Empty, 0);                      // Initialize with default values for positional record

        // GET handler: Load course details for editing
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);                                       // Fetch course by ID
            if (course == null) return NotFound();                                                          // Return 404 if not found

            Course = course;                                                                                // Bind fetched course to property
            return Page();                                                                                  // Render the page
        }

        // POST handler: Save edited course
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();                                                         // Return page if validation fails

            var result = await _courseService.UpdateCourseAsync(Course);                                    // Call API to update course
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Error updating course. Please try again.");
                return Page();
            }

            return RedirectToPage("Index");                                                                 // Redirect to Index on success
        }
    }
}
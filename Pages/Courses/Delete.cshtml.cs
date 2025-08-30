using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;

namespace StudentManagementRazorClientApp.Pages.Courses
{
    public class DeleteModel : PageModel
    {
        private readonly CourseService _courseService;                                                      // Service for API calls

        public DeleteModel(CourseService courseService)                                                     // Constructor with dependency injection of CourseService
        {
            _courseService = courseService;                                                                 // Assign injected service to local variable
        }

        [BindProperty]                                                                                      // Property bound to the form inputs on the Details page
        public CourseModel Course { get; set; } = new CourseModel(0, string.Empty, 0);                      // Initialize with default values for positional record

        // GET: Load course details to confirm deletion
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);                                       // Call the service to fetch course by its ID
            if (course == null) return NotFound();                                                          // Return a 404 Not Found response if course doesn't exist

            Course = course;                                                                                // Bind the fetched course to the PageModel property
            return Page();                                                                                  // Returns the deleted page 
        }

        // POST: Delete course
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var result = await _courseService.DeleteCourseAsync(id);                                        // Call the service to delete the course by ID
            if (!result)                                                                                    // Check if deletion failed
            {
                ModelState.AddModelError(string.Empty, "Error deleting course. Please try again.");         // Add a model-level error message
                return Page();
            }

            return RedirectToPage("Index");                                                                 // Redirect to Index after deletion
        }
    }
}
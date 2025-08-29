using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;

namespace StudentManagementRazorClientApp.Pages.Courses
{
    public class CreateModel : PageModel
    {
        private readonly CourseService _courseService;

        public CreateModel(CourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CourseModel Course { get; set; } = new CourseModel();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _courseService.AddCourseAsync(Course);
            return RedirectToPage("Index");
        }
    }
}
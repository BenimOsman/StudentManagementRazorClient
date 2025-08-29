using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;

namespace StudentManagementRazorClientApp.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        private readonly CourseService _courseService;

        public DetailsModel(CourseService courseService)
        {
            _courseService = courseService;
        }

        public CourseModel Course { get; set; } = new CourseModel();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Course = await _courseService.GetCourseByIdAsync(id);
            if (Course == null)
                return RedirectToPage("Index");

            return Page();
        }
    }
}
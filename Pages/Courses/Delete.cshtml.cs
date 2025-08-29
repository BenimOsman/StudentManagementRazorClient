using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;

namespace StudentManagementRazorClientApp.Pages.Courses
{
    public class DeleteModel : PageModel
    {
        private readonly CourseService _courseService;

        public DeleteModel(CourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CourseModel Course { get; set; } = new CourseModel();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Course = await _courseService.GetCourseByIdAsync(id);
            if (Course == null)
                return RedirectToPage("Index");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Course.CourseId != 0)
            {
                await _courseService.DeleteCourseAsync(Course.CourseId);
            }

            return RedirectToPage("Index");
        }
    }
}
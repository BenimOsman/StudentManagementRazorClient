using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;

namespace StudentManagementRazorClientApp.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly CourseService _courseService;

        public IndexModel(CourseService courseService)
        {
            _courseService = courseService;
        }

        public IList<CourseModel> Courses { get; set; } = new List<CourseModel>();

        public async Task OnGetAsync()
        {
            Courses = await _courseService.GetCoursesAsync(); // get latest data from API
        }

    }
}
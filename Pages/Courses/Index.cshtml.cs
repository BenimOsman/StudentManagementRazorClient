using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;

namespace StudentManagementRazorClientApp.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly CourseService _courseService;                                                      // Service for API calls
        
        public IndexModel(CourseService courseService)                                                      // Constructor with dependency injection of CourseService
        {
            _courseService = courseService;
        }

        public IList<CourseModel> Courses { get; set; } = new List<CourseModel>();                          // Property to hold the list of courses

        // GET request handler
        public async Task OnGetAsync()
        {
            Courses = await _courseService.GetCoursesAsync();                                               // Fetch all courses from the API and store in Courses property
        }
    }
}
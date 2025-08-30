using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;

namespace StudentManagementRazorClientApp.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly StudentService _studentService;                                                // Service for API calls

        public IndexModel(StudentService studentService)                                                // // Constructor with Dependency Injection of StudentService
        {
            _studentService = studentService;
        }

        public IList<StudentModel> Students { get; set; } = new List<StudentModel>();                   // Property to hold the list of students

        // GET request handler
        public async Task OnGetAsync()
        {
            Students = await _studentService.GetStudentsAsync();                                        // Fetch all students from the API and store in Students property
        }
    }
}
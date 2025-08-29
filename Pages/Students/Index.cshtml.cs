using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;

namespace StudentManagementRazorClientApp.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly StudentService _studentService;

        public IndexModel(StudentService studentService)
        {
            _studentService = studentService;
        }

        public IList<StudentModel> Students { get; set; } = new List<StudentModel>();

        public async Task OnGetAsync()
        {
            Students = await _studentService.GetStudentsAsync();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;

namespace StudentManagementRazorClientApp.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly StudentService _studentService;

        public DetailsModel(StudentService studentService)
        {
            _studentService = studentService;
        }

        public StudentModel Student { get; set; } = new StudentModel();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Student = await _studentService.GetStudentByIdAsync(id);
            if (Student == null)
                return NotFound();

            return Page();
        }
    }
}
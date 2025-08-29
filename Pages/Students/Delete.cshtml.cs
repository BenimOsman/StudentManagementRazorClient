using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;

namespace StudentManagementRazorClientApp.Pages.Students
{
    public class DeleteModel : PageModel
    {
        private readonly StudentService _studentService;

        public DeleteModel(StudentService studentService)
        {
            _studentService = studentService;
        }

        [BindProperty]
        public StudentModel Student { get; set; } = new StudentModel();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Student = await _studentService.GetStudentByIdAsync(id);
            if (Student == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _studentService.DeleteStudentAsync(Student.StudentId);
            return RedirectToPage("Index");
        }
    }
}
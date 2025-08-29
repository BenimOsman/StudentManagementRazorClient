using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;

namespace StudentManagementRazorClientApp.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly StudentService _studentService;

        public CreateModel(StudentService studentService)
        {
            _studentService = studentService;
        }

        [BindProperty]
        public StudentModel Student { get; set; } = new StudentModel();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _studentService.AddStudentAsync(Student);
            return RedirectToPage("Index");
        }
    }
}
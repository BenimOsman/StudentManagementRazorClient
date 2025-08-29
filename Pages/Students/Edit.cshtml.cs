using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;

namespace StudentManagementRazorClientApp.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly StudentService _studentService;

        public EditModel(StudentService studentService)
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
            if (!ModelState.IsValid)
                return Page();

            var success = await _studentService.UpdateStudentAsync(Student);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Update failed.");
                return Page();
            }

            return RedirectToPage("Index"); // back to Students list
        }
    }
}
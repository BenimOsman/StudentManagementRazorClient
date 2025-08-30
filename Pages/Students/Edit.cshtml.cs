using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;

namespace StudentManagementRazorClientApp.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly StudentService _studentService; // Service for API calls

        public EditModel(StudentService studentService)
        {
            _studentService = studentService;
        }

        // Bind the Student model to the form
        [BindProperty]
        public StudentModel Student { get; set; } = new StudentModel(0, string.Empty, 0);

        // GET: Fetch the existing student by ID
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Student = await _studentService.GetStudentByIdAsync(id);
            if (Student == null)
            {
                return RedirectToPage("Index"); // Redirect if student not found
            }
            return Page();
        }

        // POST: Update the student
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Return page if validation fails
            }

            bool isUpdated = await _studentService.UpdateStudentAsync(Student); // Call API to update

            if (!isUpdated)
            {
                ModelState.AddModelError(string.Empty, "Error updating student. Please try again.");
                return Page();
            }

            return RedirectToPage("Index"); // Redirect after successful update
        }
    }
}

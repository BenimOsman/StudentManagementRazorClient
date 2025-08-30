using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;

namespace StudentManagementRazorClientApp.Pages.Students
{
    public class DeleteModel : PageModel
    {
        private readonly StudentService _studentService;                                                    // Service to perform API operations

        public DeleteModel(StudentService studentService)                                                   // Constructor with Dependency Injection of StudentService
        {
            _studentService = studentService;
        }

        [BindProperty]                                                                                      // Property to hold student to delete
        public StudentModel Student { get; set; } = new StudentModel(0, string.Empty, 0);

        // GET handler to fetch student by ID
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Student = await _studentService.GetStudentByIdAsync(id);

            if (Student == null)
            {
                return RedirectToPage("Index");                                                             // If not found, redirect to Index
            }

            return Page();                                                                                  // Show delete confirmation
        }

        // POST handler to confirm deletion
        public async Task<IActionResult> OnPostAsync(int id)
        {
            bool isDeleted = await _studentService.DeleteStudentAsync(id);                                  // Call API to delete student

            if (!isDeleted)
            {
                ModelState.AddModelError(string.Empty, "Error deleting student. Please try again.");
                return Page();
            }

            // Redirect to Index after deletion
            return RedirectToPage("Index");
        }
    }
}
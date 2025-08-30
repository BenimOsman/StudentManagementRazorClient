using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;

namespace StudentManagementRazorClientApp.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly StudentService _studentService;                                                // Service to perform API operations

        public DetailsModel(StudentService studentService)                                              // Constructor with Dependency Injection of StudentService
        {
            _studentService = studentService;
        }

        public StudentModel Student { get; set; } = new StudentModel(0, string.Empty, 0);               // Property to hold student data

        // GET handler to fetch student details by ID
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Student = await _studentService.GetStudentByIdAsync(id);

            if (Student == null)
            {
                return RedirectToPage("Index");                                                         // If student not found, redirect to Index page
            }

            return Page();                                                                              // Return page with student details
        }
    }
}
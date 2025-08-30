using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;

namespace StudentManagementRazorClientApp.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly StudentService _studentService;                                                    // Service to call Student API

        public CreateModel(StudentService studentService)                                                   // Constructor with Dependency Injection of StudentService
        {
            _studentService = studentService;                                                               // Assign injected service to local variable
        }

        
        [BindProperty]                                                                                      // Property bound to the form data for creating a new student
        public StudentModel Student { get; set; } = new StudentModel(0, string.Empty, 0);                   // Initialize with default values for positional record

        public void OnGet()                                                                                 // GET handler called when page is requested via HTTP GET
        {
                                                                                                            // Nothing to load initially, just render the form
        }

        // POST handler called when form is submitted
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)                                                                        // Check if submitted form passes validation
            {
                return Page();                                                                              // Return same page to display validation errors
            }

            var createdStudent = await _studentService.AddStudentAsync(Student);                            // Call API to create new student

            if (createdStudent == null)                                                                     // Check if API call failed
            {
                ModelState.AddModelError(string.Empty, "Error creating student. Please try again.");        // Show error message
                return Page();                                                                              // Return page with error
            }

            return RedirectToPage("Index");                                                                 // Redirect to Students list after successful creation
        }
    }
}
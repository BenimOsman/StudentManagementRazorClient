using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagementRazorClientApp.Pages.Enrolments
{
    public class CreateModel : PageModel
    {
        private readonly EnrolmentService _enrolmentService;                                                // Service to call APIs
        private readonly StudentService _studentService;
        private readonly CourseService _courseService;

        public CreateModel(                                                                                 // Constructor with Dependency Injection
            EnrolmentService enrolmentService,
            StudentService studentService,
            CourseService courseService)
        {
            _enrolmentService = enrolmentService;                                                           // Assigned injected service to local variable
            _studentService = studentService;
            _courseService = courseService;
        }

        [BindProperty]                                                                                      // Property bound to the form data for creating a new enrolment
        public EnrolmentModel Enrolment { get; set; } = new EnrolmentModel(0, 0, 0, DateTime.Now, string.Empty);            // Initialize with default values for positional record

        public List<StudentModel> Students { get; set; } = new List<StudentModel>();                        // Initialize the students model
        public List<CourseModel> Courses { get; set; } = new List<CourseModel>();                           // Initialize the course model
            
        // GET handler (loads the page initially)
        public async Task OnGetAsync()
        {
            Students = await _studentService.GetAllStudentsAsync();
            Courses = await _courseService.GetAllCoursesAsync();
        }

        // POST handler (called when form is submitted) 
        public async Task<IActionResult> OnPostAsync()                                  
        {
            if (!ModelState.IsValid)                                                                        // Validate form inputs
                return Page();                                                                              // Return page if validation fails

            var created = await _enrolmentService.AddEnrolmentAsync(Enrolment);                             // Call the API to create the new course
            
            if (created == null)                                                                            // If API fails, show error message on page
            {
                ModelState.AddModelError(string.Empty, "Error creating enrolment.");
                return Page();
            }

            return RedirectToPage("Index");                                                                 // Redirect to Index page after successful creation
        }
    }
}
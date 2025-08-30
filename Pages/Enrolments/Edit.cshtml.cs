using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagementRazorClientApp.Pages.Enrolments
{
    public class EditModel : PageModel
    {
        private readonly EnrolmentService _enrolmentService;                                                // Service to interact with Enrolment API
        private readonly StudentService _studentService;
        private readonly CourseService _courseService;

        public EditModel(                                                                                   // Constructor with dependency injection of CourseService
            EnrolmentService enrolmentService,
            StudentService studentService,
            CourseService courseService)
        {
            _enrolmentService = enrolmentService;                                                           // Assign injected service to local variable
            _studentService = studentService;
            _courseService = courseService;
        }

        [BindProperty]                                                                                      // Properties for binding and dropdowns
        public EnrolmentModel Enrolment { get; set; } = default!;
        public List<StudentModel> Students { get; set; } = new List<StudentModel>();
        public List<CourseModel> Courses { get; set; } = new List<CourseModel>();

        // GET: Load the enrolment to edit
        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id <= 0)
                return NotFound();

            var enrolment = await _enrolmentService.GetEnrolmentByIdAsync(id);                              // Fetch enrolment from API

            if (enrolment == null)
                return NotFound();

            Enrolment = enrolment;

            // Fetch all students and courses for dropdowns
            Students = await _studentService.GetAllStudentsAsync();
            Courses = await _courseService.GetAllCoursesAsync();

            return Page();                                                                                  // Render Edit page
        }

        // POST: Save the changes
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Reload dropdowns if validation fails
                Students = await _studentService.GetAllStudentsAsync();
                Courses = await _courseService.GetAllCoursesAsync();
                return Page();
            }

            var updated = await _enrolmentService.UpdateEnrolmentAsync(Enrolment);                          // Call API to update enrolment

            if (!updated)                                                                                   // If update failed, show error
            {
                ModelState.AddModelError(string.Empty, "Error updating enrolment. Please try again.");
                Students = await _studentService.GetAllStudentsAsync();
                Courses = await _courseService.GetAllCoursesAsync();
                return Page();
            }

            return RedirectToPage("Index");                                                                 // Redirect to list after successful update
        }
    }
}
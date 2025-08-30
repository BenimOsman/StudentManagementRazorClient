using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;
using System.Threading.Tasks;

namespace StudentManagementRazorClientApp.Pages.Enrolments
{
    public class DeleteModel : PageModel
    {
        private readonly EnrolmentService _enrolmentService;                                                // Service for API calls
        private readonly StudentService _studentService;
        private readonly CourseService _courseService;

        public DeleteModel(                                                                                 // Constructor with dependency injection of CourseService
            EnrolmentService enrolmentService,
            StudentService studentService,
            CourseService courseService)
        {
            _enrolmentService = enrolmentService;
            _studentService = studentService;
            _courseService = courseService;
        }

        // Properties to hold enrolment and related names
        public EnrolmentModel Enrolment { get; set; } = default!;
        public string StudentName { get; private set; } = string.Empty;
        public string CourseName { get; private set; } = string.Empty;

        // GET: Load the enrolment to confirm deletion
        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id <= 0)
                return NotFound();

            var enrolment = await _enrolmentService.GetEnrolmentByIdAsync(id);                              // Fetch enrolment from API

            if (enrolment == null)
                return NotFound();

            Enrolment = enrolment;

            // Fetch related student and course names
            var studentTask = _studentService.GetStudentByIdAsync(enrolment.StudentId);
            var courseTask = _courseService.GetCourseByIdAsync(enrolment.CourseId);
            await Task.WhenAll(studentTask, courseTask);

            StudentName = studentTask.Result?.Name ?? $"(ID: {enrolment.StudentId})";
            CourseName = courseTask.Result?.Title ?? $"(ID: {enrolment.CourseId})";

            return Page();                                                                                  // Render Delete confirmation page
        }

        // POST: Perform the deletion
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id <= 0)
                return NotFound();

            var deleted = await _enrolmentService.DeleteEnrolmentAsync(id);                                 // Call API to delete the enrolment

            if (!deleted)                                                                                   // If deletion failed, show error
            {
                ModelState.AddModelError(string.Empty, "Error deleting enrolment. Please try again.");
                return Page();
            }

            return RedirectToPage("Index");                                                                 // Redirect to list after successful deletion
        }
    }
}
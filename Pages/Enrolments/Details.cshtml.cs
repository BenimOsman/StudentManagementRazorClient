using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;
using System.Threading.Tasks;

namespace StudentManagementRazorClientApp.Pages.Enrolments
{
    public class DetailsModel : PageModel
    {
        private readonly EnrolmentService _enrolmentService;                                                // Service for API calls
        private readonly CourseService _courseService;
        private readonly StudentService _studentService;

        public DetailsModel(                                                                                // Constructor with dependency injection of EnrolmentService
            EnrolmentService enrolmentService,
            StudentService studentService,
            CourseService courseService)
        {
            _enrolmentService = enrolmentService;                                                           // Assign service
            _studentService = studentService;
            _courseService = courseService;
        }

        public EnrolmentModel Enrolment { get; set; } = default!;
        public string StudentName { get; private set; } = string.Empty;
        public string CourseName { get; private set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id <= 0)
                return NotFound();

            var enrolment = await _enrolmentService.GetEnrolmentByIdAsync(id);                              // Fetch enrolment from API
            
            if (enrolment == null)
                return NotFound();

            Enrolment = enrolment;

            // Fetch Student and Course concurrently
            var studentTask = _studentService.GetStudentByIdAsync(enrolment.StudentId);
            var courseTask = _courseService.GetCourseByIdAsync(enrolment.CourseId);
            await Task.WhenAll(studentTask, courseTask);

            var student = studentTask.Result;
            var course = courseTask.Result;

            StudentName = student?.Name ?? $"(ID: {enrolment.StudentId})";
            CourseName = course?.Title ?? $"(ID: {enrolment.CourseId})";

            return Page();
        }
    }
}
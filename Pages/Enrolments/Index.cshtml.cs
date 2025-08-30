using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;

namespace StudentManagementRazorClientApp.Pages.Enrolments
{
    public class IndexModel : PageModel
    {
        private readonly EnrolmentService _enrolmentService;                                            // Service to call Enrolment API

        public IndexModel(EnrolmentService enrolmentService)                                            // Constructor with Dependency Injection of EnrolmentService
        {
            _enrolmentService = enrolmentService;                                                       // Assigned injected service to local variable
        }

        public IList<EnrolmentModel> Enrolments { get; set; } = new List<EnrolmentModel>();             // Property bound to the form data for creating a new enrolment

        // GET request handler
        public async Task OnGetAsync()
        {
            Enrolments = await _enrolmentService.GetEnrolmentsAsync();                                  // Fetch all enrolments including StudentName and CourseName

        }
    }
}
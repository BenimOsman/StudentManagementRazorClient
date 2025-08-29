using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;

namespace StudentManagementRazorClientApp.Pages.Enrolments
{
    public class IndexModel : PageModel
    {
        private readonly EnrolmentService _enrolmentService;

        public IndexModel(EnrolmentService enrolmentService)
        {
            _enrolmentService = enrolmentService;
        }

        public IList<EnrolmentModel> Enrolments { get; set; } = new List<EnrolmentModel>();

        [TempData]
        public string? StatusMessage { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                Enrolments = await _enrolmentService.GetEnrolmentsAsync();
            }
            catch (Exception ex)
            {
                // Don’t crash the page; surface a friendly message
                StatusMessage = "Unable to load enrolments at this time.";
                // Optionally log ex
                Enrolments = new List<EnrolmentModel>();
            }
        }
    }
}
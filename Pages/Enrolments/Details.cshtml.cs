using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;

namespace StudentManagementRazorClientApp.Pages.Enrolments
{
    public class DetailsModel : PageModel
    {
        private readonly EnrolmentService _enrolmentService;

        public DetailsModel(EnrolmentService enrolmentService)
        {
            _enrolmentService = enrolmentService;
        }

        public EnrolmentModel? Enrolment { get; set; }

        [TempData]
        public string? StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Enrolment = await _enrolmentService.GetEnrolmentByIdAsync(id);
            if (Enrolment == null)
            {
                StatusMessage = "Enrolment not found.";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
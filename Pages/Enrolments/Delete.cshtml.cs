using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;

namespace StudentManagementRazorClientApp.Pages.Enrolments
{
    public class DeleteModel : PageModel
    {
        private readonly EnrolmentService _enrolmentService;

        public DeleteModel(EnrolmentService enrolmentService)
        {
            _enrolmentService = enrolmentService;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            // prefer id from route; hidden input is a fallback
            var success = await _enrolmentService.DeleteEnrolmentAsync(id);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Unable to delete the enrolment.");
                // Reload to show details again
                Enrolment = await _enrolmentService.GetEnrolmentByIdAsync(id);
                return Page();
            }

            StatusMessage = "Enrolment deleted successfully.";
            return RedirectToPage("Index");
        }
    }
}
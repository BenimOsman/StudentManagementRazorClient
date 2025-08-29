using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;

namespace StudentManagementRazorClientApp.Pages.Enrolments
{
    public class EditModel : PageModel
    {
        private readonly EnrolmentService _enrolmentService;

        public EditModel(EnrolmentService enrolmentService)
        {
            _enrolmentService = enrolmentService;
        }

        [BindProperty]
        public EnrolmentModel Enrolment { get; set; } = default!;

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
            if (!ModelState.IsValid)
                return Page();

            if (Enrolment.EnrolmentId == 0)
                Enrolment.EnrolmentId = id;

            var updated = await _enrolmentService.UpdateEnrolmentAsync(Enrolment);
            if (updated == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to update the enrolment.");
                return Page();
            }

            StatusMessage = "Enrolment updated successfully.";
            return RedirectToPage("Index");
        }
    }
}
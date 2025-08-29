using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;

namespace StudentManagementRazorClientApp.Pages.Enrolments
{
    public class CreateModel : PageModel
    {
        private readonly EnrolmentService _enrolmentService;

        public CreateModel(EnrolmentService enrolmentService)
        {
            _enrolmentService = enrolmentService;
        }

        [BindProperty]
        public EnrolmentModel Enrolment { get; set; } = new();

        [TempData]
        public string? StatusMessage { get; set; }

        public void OnGet()
        {
            // Default date for UX
            if (Enrolment.JoiningDate == default)
                Enrolment.JoiningDate = DateTime.Today;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var created = await _enrolmentService.AddEnrolmentAsync(Enrolment);
            if (created == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to create enrolment.");
                return Page();
            }

            StatusMessage = "Enrolment created successfully.";
            return RedirectToPage("Index");
        }
    }
}
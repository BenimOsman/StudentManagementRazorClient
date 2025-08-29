using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp.Models;
using StudentManagementRazorClientApp.Services;

namespace StudentManagementRazorClientApp.Pages.Courses
{
    public class EditModel : PageModel
    {
        private readonly CourseService _courseService;

        public EditModel(CourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CourseModel Course { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Course = await _courseService.GetCourseByIdAsync(id);
            if (Course == null)
            {
                // Could also return NotFound();
                return RedirectToPage("Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
                return Page();

            // Ensure the key is present (covers case hidden input is missing)
            if (Course.CourseId == 0)
                Course.CourseId = id;

            bool updated = await _courseService.UpdateCourseAsync(Course);
            if (!updated)
            {
                ModelState.AddModelError(string.Empty, "Unable to update the course.");
                return Page(); // <-- stay on edit to show the error
            }

            // Optional UX: show a success toast/message on Index
            TempData["StatusMessage"] = "Course updated successfully.";
            return RedirectToPage("Index");
        }
    }
}
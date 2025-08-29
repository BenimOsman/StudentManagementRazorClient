using System.Collections.Generic;

namespace StudentManagementRazorClientApp.Models
{
    public class CourseModel
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }

        // Virtual property for potential navigation
        public virtual ICollection<EnrolmentModel>? Enrolments { get; set; }
    }
}
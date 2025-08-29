using System.Collections.Generic;

namespace StudentManagementRazorClientApp.Models
{
    public class StudentModel
    {
        public int StudentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }

        // Virtual property for potential navigation
        public virtual ICollection<EnrolmentModel>? Enrolments { get; set; }
    }
}
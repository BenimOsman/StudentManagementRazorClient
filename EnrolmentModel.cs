using System;

namespace StudentManagementRazorClientApp.Models
{
    public class EnrolmentModel
    {
        public int EnrolmentId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime JoiningDate { get; set; }
        public string Grade { get; set; } = string.Empty;

        // Virtual navigation properties
        public virtual StudentModel? Student { get; set; }
        public virtual CourseModel? Course { get; set; }
    }
}
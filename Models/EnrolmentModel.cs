using System.Text.Json.Serialization;

namespace StudentManagementRazorClientApp.Models
{
    public record class EnrolmentModel(
        [property: JsonPropertyName("enrolmentId")] int EnrolmentId,                                    // Maps "enrolmentId" from JSON → EnrolmentId property in C#

        [property: JsonPropertyName("studentId")] int StudentId,                                        // Maps "studentId" from JSON → StudentId property in C#

        [property: JsonPropertyName("courseId")] int CourseId,                                          // Maps "courseId" from JSON → CourseId property in C#

        [property: JsonPropertyName("joiningDate")] DateTime JoiningDate,                               // Maps "joiningDate" from JSON → JoiningDate property in C#

        [property: JsonPropertyName("grade")] string Grade                                              // Maps "grade" from JSON → Grade property in C#
    )
    {
        // These are *inside* the record and use 'init' to preserve immutability.
        [property: JsonPropertyName("studentName")]
        public string? StudentName { get; init; }

        [property: JsonPropertyName("courseName")]
        public string? CourseName { get; init; }
    }
}

// Record types are immutable i.e. Once defined it can't change
// Records compare values, not references
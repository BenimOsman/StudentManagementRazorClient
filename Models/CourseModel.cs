using System.Text.Json.Serialization;

namespace StudentManagementRazorClientApp.Models
{
    public record class CourseModel(
        [property: JsonPropertyName("courseId")] int CourseId,                                  // Maps "courseId" from JSON → CourseId property in C#

        [property: JsonPropertyName("title")] string Title,                                     // Maps "title" from JSON → Title property in C#

        [property: JsonPropertyName("credits")] int Credits                                     // Maps "credits" from JSON → Credits property in C#
    );
}

// Record types are immutable i.e. Once defined it can't change
// Records compare values, not references
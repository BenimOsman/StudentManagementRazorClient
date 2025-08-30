using System.Text.Json.Serialization;

namespace StudentManagementRazorClientApp.Models
{
    public record class StudentModel
    (
        [property: JsonPropertyName("studentId")] int StudentId,                                        // Maps "studentId" from JSON → StudentId property in C#

        [property: JsonPropertyName("name")] string Name,                                               // Maps "name" from JSON → Name property in C#

        [property: JsonPropertyName("age")] int Age                                                     // Maps "age" from JSON → Age property in C#
    );
}

// Record types are immutable i.e. Once defined it can't change
// Records compare values, not references
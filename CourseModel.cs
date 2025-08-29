using System.Text.Json.Serialization;

namespace StudentManagementRazorClientApp.Models
{
    public class CourseModel
    {
        [JsonPropertyName("courseId")] // <-- matches API JSON
        public int CourseId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("credits")]
        public int Credits { get; set; }
    }
}
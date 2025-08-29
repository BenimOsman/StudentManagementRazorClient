using StudentManagementRazorClientApp.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace StudentManagementRazorClientApp.Services
{
    public class CourseService
    {
        private readonly string baseUrl = "https://localhost:7275/api/Course";

        private HttpClient GetHttpClient()
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            return new HttpClient(handler);
        }

        public async Task<List<CourseModel>> GetCoursesAsync()
        {
            using HttpClient client = GetHttpClient();
            await using Stream stream = await client.GetStreamAsync(baseUrl);
            return await JsonSerializer.DeserializeAsync<List<CourseModel>>(stream) ?? new List<CourseModel>();
        }

        public async Task<CourseModel?> GetCourseByIdAsync(int id)
        {
            using HttpClient client = GetHttpClient();
            var response = await client.GetAsync($"{baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<CourseModel>();
        }

        public async Task<CourseModel?> AddCourseAsync(CourseModel course)
        {
            using HttpClient client = GetHttpClient();
            var response = await client.PostAsJsonAsync(baseUrl, course);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<CourseModel>();
        }

        public async Task<CourseModel?> UpdateCourseAsync(CourseModel course)
        {
            using HttpClient client = GetHttpClient();
            var response = await client.PutAsJsonAsync($"{baseUrl}/{course.CourseId}", course);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<CourseModel>();
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            using HttpClient client = GetHttpClient();
            var response = await client.DeleteAsync($"{baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
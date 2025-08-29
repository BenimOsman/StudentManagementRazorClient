using StudentManagementRazorClientApp.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace StudentManagementRazorClientApp.Services
{
    public class StudentService
    {
        private readonly string baseUrl = "https://localhost:7275/api/Student";

        private HttpClient GetHttpClient()
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            return new HttpClient(handler);
        }

        // Get all students
        public async Task<List<StudentModel>> GetStudentsAsync()
        {
            try
            {
                using HttpClient client = GetHttpClient();
                await using var stream = await client.GetStreamAsync(baseUrl);
                return await JsonSerializer.DeserializeAsync<List<StudentModel>>(
                    stream,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                ) ?? new List<StudentModel>();
            }
            catch
            {
                throw;
            }
        }

        // Get student by ID
        public async Task<StudentModel?> GetStudentByIdAsync(int id)
        {
            using HttpClient client = GetHttpClient();
            var response = await client.GetAsync($"{baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<StudentModel>(
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
        }

        // Add a new student
        public async Task<StudentModel?> AddStudentAsync(StudentModel student)
        {
            using HttpClient client = GetHttpClient();
            var response = await client.PostAsJsonAsync(baseUrl, student);
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<StudentModel>(
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
        }

        // Update existing student
        public async Task<bool> UpdateStudentAsync(StudentModel student)
        {
            using HttpClient client = GetHttpClient();
            var response = await client.PutAsJsonAsync($"{baseUrl}/{student.StudentId}", student);

            return response.IsSuccessStatusCode; // returns true if 200/204
        }

        // Delete student
        public async Task<bool> DeleteStudentAsync(int id)
        {
            using HttpClient client = GetHttpClient();
            var response = await client.DeleteAsync($"{baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
using StudentManagementRazorClientApp.Models;                                                       // Import the StudentModel and other related models
using System.Text.Json;                                                                             // For JsonSerializer and options

namespace StudentManagementRazorClientApp
{
    public class StudentService
    {
        private readonly string baseUrl = "https://localhost:7275/api/Student";                     // Base URL of your Student Web API

        private HttpClient GetHttpClient()                                                          // Method to create a HttpClient that ignores SSL certificate errors
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true  // Not preferred
            };
            return new HttpClient(handler);
        }

        // Get all students from API
        public async Task<List<StudentModel>> GetStudentsAsync()
        {
            try
            {
                using HttpClient client = GetHttpClient();                                          // Get a HttpClient instance
                await using var stream = await client.GetStreamAsync(baseUrl);                      // Get response as stream (raw JSON) from API
 
                return await JsonSerializer.DeserializeAsync<List<StudentModel>>(                   // Deserialize JSON into List<StudentModel>
                    stream,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                ) ?? new List<StudentModel>();                                                      // Return empty list if null
            } catch {
                throw; 
            }
        }

        
        // Get a student by ID
        public async Task<StudentModel?> GetStudentByIdAsync(int id)
        {
            using HttpClient client = GetHttpClient();                                              // Get a HttpClient instance
            var response = await client.GetAsync($"{baseUrl}/{id}");                                // GET https://.../api/Student/{id}

            if (!response.IsSuccessStatusCode) return null;                                         // Return null if not found

            return await response.Content.ReadFromJsonAsync<StudentModel>(                          // Deserialize JSON response into a single StudentModel
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
        }

        // Add a new student
        public async Task<StudentModel?> AddStudentAsync(StudentModel student)
        {
            using HttpClient client = GetHttpClient();                                              // Get a HttpClient instance
            var response = await client.PostAsJsonAsync(baseUrl, student);                          // POST student object as JSON to API

            if (!response.IsSuccessStatusCode) return null;                                         // Return null if API call fails

            return await response.Content.ReadFromJsonAsync<StudentModel>(                          // Deserialize response JSON back into StudentModel 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
        }

        // Update existing student
        public async Task<bool> UpdateStudentAsync(StudentModel student)
        {
            using HttpClient client = GetHttpClient();                                              // Get a HttpClient instance
            var response = await client.PutAsJsonAsync($"{baseUrl}/{student.StudentId}", student);  // PUT request to update the student with given ID
            return response.IsSuccessStatusCode;                                                    // Return true if update succeeded (HTTP 200/204)
        }

        // Delete a student
        public async Task<bool> DeleteStudentAsync(int id)
        {
            using HttpClient client = GetHttpClient();
            var response = await client.DeleteAsync($"{baseUrl}/{id}");                             // DELETE request to API
            return response.IsSuccessStatusCode;                                                    // Return true if deletion succeeded
        }
    }
}
using StudentManagementRazorClientApp.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace StudentManagementRazorClientApp.Services
{
    public class EnrolmentService
    {
        private readonly string baseUrl = "https://localhost:7275/api/Enrolment";

        // 🔧 Centralize JSON options
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,      // read camelCase/PascalCase
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase // write camelCase
        };

        private HttpClient GetHttpClient()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (m, cert, chain, errors) => true
            };
            return new HttpClient(handler);
        }

        public async Task<List<EnrolmentModel>> GetEnrolmentsAsync()
        {
            using HttpClient client = GetHttpClient();

            // Option A (safer for content-types): use GetStringAsync then deserialize
            var json = await client.GetStringAsync(baseUrl);
            return JsonSerializer.Deserialize<List<EnrolmentModel>>(json, _jsonOptions)
                   ?? new List<EnrolmentModel>();

            // Option B (also fine): use stream + options
            // await using var stream = await client.GetStreamAsync(baseUrl);
            // return await JsonSerializer.DeserializeAsync<List<EnrolmentModel>>(stream, _jsonOptions)
            //        ?? new List<EnrolmentModel>();
        }

        public async Task<EnrolmentModel?> GetEnrolmentByIdAsync(int id)
        {
            using HttpClient client = GetHttpClient();
            var response = await client.GetAsync($"{baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<EnrolmentModel>(_jsonOptions);
        }

        public async Task<EnrolmentModel?> AddEnrolmentAsync(EnrolmentModel enrolment)
        {
            using HttpClient client = GetHttpClient();

            // ensure outbound JSON is camelCase
            var response = await client.PostAsJsonAsync(baseUrl, enrolment, _jsonOptions);
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<EnrolmentModel>(_jsonOptions);
        }

        public async Task<EnrolmentModel?> UpdateEnrolmentAsync(EnrolmentModel enrolment)
        {
            using HttpClient client = GetHttpClient();

            var response = await client.PutAsJsonAsync($"{baseUrl}/{enrolment.EnrolmentId}", enrolment, _jsonOptions);
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<EnrolmentModel>(_jsonOptions);
        }

        public async Task<bool> DeleteEnrolmentAsync(int id)
        {
            using HttpClient client = GetHttpClient();
            var response = await client.DeleteAsync($"{baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
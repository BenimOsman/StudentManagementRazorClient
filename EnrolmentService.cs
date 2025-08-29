using StudentManagementRazorClientApp.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace StudentManagementRazorClientApp.Services
{
    public class EnrolmentService
    {
        private readonly string baseUrl = "https://localhost:7275/api/Enrolment";

        private HttpClient GetHttpClient()
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            return new HttpClient(handler);
        }

        public async Task<List<EnrolmentModel>> GetEnrolmentsAsync()
        {
            using HttpClient client = GetHttpClient();
            await using Stream stream = await client.GetStreamAsync(baseUrl);
            return await JsonSerializer.DeserializeAsync<List<EnrolmentModel>>(stream) ?? new List<EnrolmentModel>();
        }

        public async Task<EnrolmentModel?> GetEnrolmentByIdAsync(int id)
        {
            using HttpClient client = GetHttpClient();
            var response = await client.GetAsync($"{baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<EnrolmentModel>();
        }

        public async Task<EnrolmentModel?> AddEnrolmentAsync(EnrolmentModel enrolment)
        {
            using HttpClient client = GetHttpClient();
            var response = await client.PostAsJsonAsync(baseUrl, enrolment);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<EnrolmentModel>();
        }

        public async Task<EnrolmentModel?> UpdateEnrolmentAsync(EnrolmentModel enrolment)
        {
            using HttpClient client = GetHttpClient();
            var response = await client.PutAsJsonAsync($"{baseUrl}/{enrolment.EnrolmentId}", enrolment);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<EnrolmentModel>();
        }

        public async Task<bool> DeleteEnrolmentAsync(int id)
        {
            using HttpClient client = GetHttpClient();
            var response = await client.DeleteAsync($"{baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
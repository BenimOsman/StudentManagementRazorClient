using StudentManagementRazorClientApp.Models;                                                               // Import models from the project namespace
using System.Text.Json;

namespace StudentManagementRazorClientApp.Services
{
    public class EnrolmentService                                                                           // Service class to handle all HTTP operations related to Enrolments
    {
        // 🔧 Centralize JSON options
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,                                                             // Allows mapping camelCase JSON to PascalCase C# properties
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private readonly HttpClient _httpClient;                                                            // HttpClient instance used to call the API

        public EnrolmentService(HttpClient httpClient)                                                      // Constructor with dependency injection of HttpClient
        {
            _httpClient = httpClient;                                                                       // Assign injected HttpClient to local variable
        }

        // GET all enrolments from API
        public async Task<List<EnrolmentModel>> GetEnrolmentsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<EnrolmentModel>>("api/Enrolment")                // Calls GET api/Enrolment → returns list of enrolments
                   ?? new List<EnrolmentModel>();                                                           // If null, return an empty list
        }

        // GET single enrolment by ID
        public async Task<EnrolmentModel?> GetEnrolmentByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<EnrolmentModel>($"api/Enrolment/{id}");               // Calls GET api/Enrolment/{id} → returns one enrolment
        }

        // CREATE a new enrolment
        public async Task<EnrolmentModel?> AddEnrolmentAsync(EnrolmentModel enrolment)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Enrolment", enrolment);                   // Calls POST api/Enrolment with enrolment data
            if (!response.IsSuccessStatusCode) return null;                                                 // If failed, return null

            return await response.Content.ReadFromJsonAsync<EnrolmentModel>();                              // If success, read and return created enrolment object
        }

        // UPDATE an existing enrolment
        public async Task<bool> UpdateEnrolmentAsync(EnrolmentModel enrolment)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Enrolment/{enrolment.EnrolmentId}",       // Calls PUT api/Enrolment/{id} with updated data 
                enrolment);   
            return response.IsSuccessStatusCode;                                                            // Return true if update was successful
        }

        // DELETE enrolment by ID
        public async Task<bool> DeleteEnrolmentAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Enrolment/{id}");                            // Calls DELETE api/Enrolment/{id}
            return response.IsSuccessStatusCode;                                                            // Return true if deletion succeeded
        }
    }
}
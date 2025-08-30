using StudentManagementRazorClientApp.Models;                                                               // Import models from the project namespace

namespace StudentManagementRazorClientApp.Services
{
    public class CourseService                                                                              // Service class to handle all HTTP operations related to Courses
    {
        private readonly HttpClient _httpClient;                                                            // HttpClient instance used to call the API

        public CourseService(HttpClient httpClient)                                                         // Constructor with dependency injection of HttpClient
        {
            _httpClient = httpClient;                                                                       // Assign injected HttpClient to local variable
        }

        // GET all courses from API
        public async Task<List<CourseModel>> GetCoursesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CourseModel>>("api/Course")                      // Calls GET api/Course → returns list of courses
                   ?? new List<CourseModel>();                                                              // If null, return an empty list
        }

        // CourseService.cs
        public async Task<List<CourseModel>> GetAllCoursesAsync()
        {
            var courses = await _httpClient.GetFromJsonAsync<List<CourseModel>>("api/Course");              // Call the API endpoint "api/Course" to get all courses
            return courses ?? new List<CourseModel>();                                                      // Ensure the returned value is never null. If API fails or returns null, return empty list
        }

        // GET single course by ID
        public async Task<CourseModel?> GetCourseByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<CourseModel>($"api/Course/{id}");                     // Calls GET api/Course/{id} → returns one course
        }

        // CREATE a new course
        public async Task<CourseModel?> AddCourseAsync(CourseModel course)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Course", course);                         // Calls POST api/Course with course data
            if (!response.IsSuccessStatusCode) return null;                                                 // If failed, return null

            return await response.Content.ReadFromJsonAsync<CourseModel>();                                 // If success, read and return created course object
        }

        // UPDATE an existing course
        public async Task<bool> UpdateCourseAsync(CourseModel course)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Course/{course.CourseId}", course);       // Calls PUT api/Course/{id} with updated data
            return response.IsSuccessStatusCode;                                                            // Return true if update was successful
        }

        // DELETE course by ID
        public async Task<bool> DeleteCourseAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Course/{id}");                               // Calls DELETE api/Course/{id}
            return response.IsSuccessStatusCode;                                                            // Return true if deletion succeeded
        }
    }
}
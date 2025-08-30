using StudentManagementRazorClientApp.Models;                                                               // Import models from the project namespace

namespace StudentManagementRazorClientApp.Services
{
    public class StudentService                                                                             // Service class to handle all HTTP operations related to Students
    {
        private readonly HttpClient _httpClient;                                                            // HttpClient instance used to call the API

        public StudentService(HttpClient httpClient)                                                        // Constructor with dependency injection of HttpClient
        {
            _httpClient = httpClient;                                                                       // Assign injected HttpClient to local variable
        }

        // GET all students from API
        public async Task<List<StudentModel>> GetStudentsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<StudentModel>>("api/Student")                    // Calls GET api/Student → returns list of students
                   ?? new List<StudentModel>();                                                             // If null, return an empty list
        }

        // StudentService.cs
        public async Task<List<StudentModel>> GetAllStudentsAsync()
        {
            var students = await _httpClient.GetFromJsonAsync<List<StudentModel>>("api/Student");           // Call the API endpoint "api/Student" to get all students
            return students ?? new List<StudentModel>();                                                    // Ensure the returned value is never null. If API fails or returns null, return empty list
        }

        // GET single student by ID
        public async Task<StudentModel?> GetStudentByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<StudentModel>($"api/Student/{id}");                   // Calls GET api/Student/{id} → returns one student
        }

        // CREATE a new student
        public async Task<StudentModel?> AddStudentAsync(StudentModel student)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Student", student);                       // Calls POST api/Student with student data
            if (!response.IsSuccessStatusCode) return null;                                                 // If failed, return null

            return await response.Content.ReadFromJsonAsync<StudentModel>();                                // If success, read and return created student object
        }

        // UPDATE an existing student
        public async Task<bool> UpdateStudentAsync(StudentModel student)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Student/{student.StudentId}", student);   // Calls PUT api/Student/{id} with updated data
            return response.IsSuccessStatusCode;                                                            // Return true if update was successful
        }

        // DELETE student by ID
        public async Task<bool> DeleteStudentAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Student/{id}");                              // Calls DELETE api/Student/{id}
            return response.IsSuccessStatusCode;                                                            // Return true if deletion succeeded
        }
    }
}
using StudentManagementRazorClientApp.Services;                                             // Import the service classes (Student, Course, Enrolment) for DI

var builder = WebApplication.CreateBuilder(args);                                           // Initialize a new WebApplicationBuilder instance

builder.Services.AddRazorPages();                                                           // Add Razor Pages 

// Register StudentService with HttpClient for DI
builder.Services.AddHttpClient<StudentService>(client =>                                    // Adds StudentService to DI container with a configured HttpClient
{
    client.BaseAddress = new Uri("https://localhost:7275/");                                // Base URI for Student API endpoints
});

// Register CourseService with HttpClient for DI
builder.Services.AddHttpClient<CourseService>(client =>                                     // Adds CourseService to DI container with a configured HttpClient
{
    client.BaseAddress = new Uri("https://localhost:7275/");                                // Base URI for Course API endpoints
});

// Register EnrolmentService with HttpClient for DI
builder.Services.AddHttpClient<EnrolmentService>(client =>                                  // Adds EnrolmentService to DI container with a configured HttpClient
{
    client.BaseAddress = new Uri("https://localhost:7275/");                                // Base URI for Enrolment API endpoints
});

var app = builder.Build();                                                                  // Build the WebApplication instance

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())                                                       // Check if the application is NOT in development mode
{
    app.UseExceptionHandler("/Error");                                                      // Use the custom error page for exceptions
    app.UseHsts();                                                                          
}

app.UseHttpsRedirection();                                                                  // Redirect HTTP requests to HTTPS
app.UseStaticFiles();                                                                       // Enable serving static files (CSS, JS, images, etc.)
app.UseRouting();                                                                           // Enable routing to endpoints
app.UseAuthorization();                                                                     // Enable authorization middleware
app.MapRazorPages();                                                                        // Map Razor Pages endpoints to routes
app.Run();                                                                                  // Run the application
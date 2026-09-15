using Blazor_StudentInfoApp.Components;
using Blazor_StudentInfoApp.Models;
using Blazor_StudentInfoApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add Blazor services.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Chapter 14 DI example.
builder.Services.AddScoped<IMessageService, MessageService>();

// Chapter 16 Student API example.
builder.Services.AddSingleton<StudentStore>();

// HttpClient used by the Blazor component.
builder.Services.AddScoped(_ => new HttpClient());
builder.Services.AddSingleton<StudentStore>();

builder.Services.AddScoped(
    _ => new HttpClient());

builder.Services.AddScoped<StudentApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();


// ----------------------------------------------------
// Student API
// ----------------------------------------------------

// GET: api/students
app.MapGet("/api/students",
    (StudentStore store) =>
    {
        var students = store.GetAll();

        return Results.Ok(students);
    });


// POST: api/students
app.MapPost("/api/students",
    (Student student, StudentStore store) =>
    {
        if (string.IsNullOrWhiteSpace(student.Name))
        {
            return Results.BadRequest(
                "Student name is required.");
        }

        if (string.IsNullOrWhiteSpace(student.Course))
        {
            return Results.BadRequest(
                "Course is required.");
        }

        student.Name = student.Name.Trim();
        student.Course = student.Course.Trim();

        Student createdStudent = store.Add(student);

        return Results.Created(
            $"/api/students/{createdStudent.Id}",
            createdStudent);
    });


// Blazor application.
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
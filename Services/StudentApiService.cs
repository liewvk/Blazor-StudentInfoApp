using System.Net;
using System.Net.Http.Json;
using Blazor_StudentInfoApp.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor_StudentInfoApp.Services
{
    public class StudentApiService
    {
        private readonly HttpClient _http;


        public StudentApiService(
            HttpClient http,
            NavigationManager navigation)
        {
            _http = http;

            if (_http.BaseAddress == null)
            {
                _http.BaseAddress =
                    new Uri(navigation.BaseUri);
            }
        }


        public async Task<List<Student>>
            GetStudentsAsync()
        {
            return await _http
                .GetFromJsonAsync<List<Student>>(
                    "api/students")
                ?? new List<Student>();
        }


        public async Task<Student?>
            GetStudentAsync(int id)
        {
            HttpResponseMessage response =
                await _http.GetAsync(
                    $"api/students/{id}");

            if (response.StatusCode ==
                HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadFromJsonAsync<Student>();
        }


        public async Task<Student?>
            CreateStudentAsync(Student student)
        {
            HttpResponseMessage response =
                await _http.PostAsJsonAsync(
                    "api/students",
                    student);

            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadFromJsonAsync<Student>();
        }


        public async Task UpdateStudentAsync(
            Student student)
        {
            HttpResponseMessage response =
                await _http.PutAsJsonAsync(
                    $"api/students/{student.Id}",
                    student);

            response.EnsureSuccessStatusCode();
        }


        public async Task DeleteStudentAsync(
            int id)
        {
            HttpResponseMessage response =
                await _http.DeleteAsync(
                    $"api/students/{id}");

            response.EnsureSuccessStatusCode();
        }
    }
}
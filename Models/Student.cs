using System.ComponentModel.DataAnnotations;

namespace Blazor_StudentInfoApp.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Student name is required.")]
        [StringLength(
            100,
            ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Course is required.")]
        [StringLength(
            100,
            ErrorMessage = "Course cannot exceed 100 characters.")]
        public string Course { get; set; } = "";
    }
}
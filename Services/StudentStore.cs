using Blazor_StudentInfoApp.Models;

namespace Blazor_StudentInfoApp.Services
{
    public class StudentStore
    {
        private readonly List<Student> _students = new()
        {
            new Student
            {
                Id = 1,
                Name = "Alice Tan",
                Course = "Blazor"
            },

            new Student
            {
                Id = 2,
                Name = "John Lee",
                Course = "C#"
            },

            new Student
            {
                Id = 3,
                Name = "Siti Aminah",
                Course = "ASP.NET Core"
            },

            new Student
            {
                Id = 4,
                Name = "Rahul Kumar",
                Course = "Blazor"
            }
        };

        private int _nextId = 5;


        public List<Student> GetAll()
        {
            return _students
                .OrderBy(student => student.Id)
                .ToList();
        }


        public Student? GetById(int id)
        {
            return _students
                .FirstOrDefault(student => student.Id == id);
        }


        public Student Add(Student student)
        {
            Student newStudent = new()
            {
                Id = _nextId++,
                Name = student.Name.Trim(),
                Course = student.Course.Trim()
            };

            _students.Add(newStudent);

            return newStudent;
        }


        public bool Update(int id, Student updatedStudent)
        {
            Student? existingStudent =
                GetById(id);

            if (existingStudent == null)
            {
                return false;
            }

            existingStudent.Name =
                updatedStudent.Name.Trim();

            existingStudent.Course =
                updatedStudent.Course.Trim();

            return true;
        }


        public bool Delete(int id)
        {
            Student? student =
                GetById(id);

            if (student == null)
            {
                return false;
            }

            _students.Remove(student);

            return true;
        }
    }
}
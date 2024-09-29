using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public interface ILearningFactory
{
    Student CreateStudent(int id, string name);
    Teacher CreateTeacher(int id, string name, int experience);
    Course CreateCourse(int id, string name, int teacherId, List<int> studentIds);
}

public class LearningFactory : ILearningFactory
{
    public Student CreateStudent(int id, string name)
    {
        return new Student(id, name);
    }

    public Teacher CreateTeacher(int id, string name, int experience)
    {
        return new Teacher(id, name, experience);
    }

    public Course CreateCourse(int id, string name, int teacherId, List<int> studentIds)
    {
        return new Course(id, name, teacherId, studentIds);
    }
}

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<int> Courses { get; set; }

    public Student(int id, string name)
    {
        Id = id;
        Name = name;
        Courses = new List<int>();
    }

    public override string ToString()
    {
        return $"Student Id = {Id}, Name = {Name}, Courses = {string.Join(",", Courses)}";
    }
}

public class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Experience { get; set; }
    public List<int> Courses { get; set; }

    public Teacher(int id, string name, int experience)
    {
        Id = id;
        Name = name;
        Experience = experience;
        Courses = new List<int>();
    }

    public override string ToString()
    {
        return $"Teacher Id = {Id}, Name = {Name}, Exp = {Experience}, Courses = {string.Join(",", Courses)}";
    }
}

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int TeacherId { get; set; }
    public List<int> StudentIds { get; set; }

    public Course(int id, string name, int teacherId, List<int> studentIds)
    {
        Id = id;
        Name = name;
        TeacherId = teacherId;
        StudentIds = studentIds;
    }

    public override string ToString()
    {
        return $"Course Id = {Id}, Name = {Name}, Teacher id = {TeacherId}, Students id = {string.Join(",", StudentIds)}";
    }
}

public class DataHandler
{
    public void SaveToFile(string filePath, List<Student> students, List<Teacher> teachers, List<Course> courses)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var student in students)
            {
                writer.WriteLine($"student,{student.Id},{student.Name},{string.Join(";", student.Courses)}");
            }

            foreach (var teacher in teachers)
            {
                writer.WriteLine($"teacher,{teacher.Id},{teacher.Name},{teacher.Experience},{string.Join(";", teacher.Courses)}");
            }

            foreach (var course in courses)
            {
                writer.WriteLine($"course,{course.Id},{course.Name},{course.TeacherId},{string.Join(";", course.StudentIds)}");
            }
        }
    }

    public void LoadFromFile(string filePath, out List<Student> students, out List<Teacher> teachers, out List<Course> courses)
    {
        students = new List<Student>();
        teachers = new List<Teacher>();
        courses = new List<Course>();

        string[] lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            string[] parts = line.Split(',');

            if (parts[0] == "student")
            {
                int id = int.Parse(parts[1]);
                string name = parts[2];
                List<int> courseIds = parts[3].Split(';').Select(int.Parse).ToList();
                var student = new Student(id, name) { Courses = courseIds };
                students.Add(student);
            }
            else if (parts[0] == "teacher")
            {
                int id = int.Parse(parts[1]);
                string name = parts[2];
                int experience = int.Parse(parts[3]);
                List<int> courseIds = parts[4].Split(';').Select(int.Parse).ToList();
                var teacher = new Teacher(id, name, experience) { Courses = courseIds };
                teachers.Add(teacher);
            }
            else if (parts[0] == "course")
            {
                int id = int.Parse(parts[1]);
                string name = parts[2];
                int teacherId = int.Parse(parts[3]);
                List<int> studentIds = parts[4].Split(';').Select(int.Parse).ToList();
                var course = new Course(id, name, teacherId, studentIds);
                courses.Add(course);
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        ILearningFactory factory = new LearningFactory();

        // Создание студентов, преподавателей и курсов
        var student1 = factory.CreateStudent(1, "Alice");
        var student2 = factory.CreateStudent(2, "Bob");

        var teacher1 = factory.CreateTeacher(1, "Dr. Smith", 10);

        var course1 = factory.CreateCourse(1, "Math", teacher1.Id, new List<int> { student1.Id, student2.Id });

        // Связывание объектов
        student1.Courses.Add(course1.Id);
        student2.Courses.Add(course1.Id);
        teacher1.Courses.Add(course1.Id);

        // Сохранение в файл
        DataHandler dataHandler = new DataHandler();
        dataHandler.SaveToFile("data.txt", new List<Student> { student1, student2 }, new List<Teacher> { teacher1 }, new List<Course> { course1 });

        // Загрузка из файла
        dataHandler.LoadFromFile("data.txt", out var loadedStudents, out var loadedTeachers, out var loadedCourses);

        Console.WriteLine("Данные загружены из файла:");

        foreach (var course in loadedCourses)
        {
            Console.WriteLine(course);
        }

        foreach (var student in loadedStudents)
        {
            Console.WriteLine(student);
        }

        foreach (var teacher in loadedTeachers)
        {
            Console.WriteLine(teacher);
        }
    }
}

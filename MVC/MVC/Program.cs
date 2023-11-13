// See https://aka.ms/new-console-template for more information
using System.Data.Entity;
using System.Diagnostics;
#nullable disable

Console.WriteLine("Hello, World!");
using (var context = new MyContext())
{
    // Create and save a new Students
    Console.WriteLine("Adding new students");

    context.Database.ExecuteSqlCommand("GRANT ALTER, UPDATE, DELETE, INSERT ON Mycontext.dbo.students TO [DELL]");
    //context.Database.ExecuteSqlCommand("GRANT ALTER, UPDATE, DELETE, INSERT ON Mycontext.dbo.COURSES TO [DESKTOP-BRJG684]");
    //context.Database.ExecuteSqlCommand("GRANT ALTER, UPDATE, DELETE, INSERT ON Mycontext.dbo.enrollments TO [DESKTOP-BRJG684]");


    var student = new Student
    {
        FirstMidName = "Atyia",
        LastName = "Alam",
        Address = "167-H",
        DOB = "13 Jan, 2002",
        EnrollmentDate = DateTime.Parse(DateTime.Today.ToString())
    };

    context.Students.Add(student);

    var student1 = new Student
    {
        FirstMidName = "Ali",
        LastName = "Ahmed",
        Address = "168-H",
        DOB = "12 January, 2002",
        EnrollmentDate = DateTime.Parse(DateTime.Today.ToString())
    };

    context.Students.Add(student1);
    context.SaveChanges();

    // Display all Students from the database
    var students = (from s in context.Students
                    orderby s.FirstMidName
                    select s).ToList<Student>();

    Console.WriteLine("Retrieve all Students from the database:");

    foreach (var stdnt in students)
    {
        string name = stdnt.FirstMidName + " " + stdnt.LastName;
        Console.WriteLine("ID: {0}, Name: {1}", stdnt.ID, name);
    }

    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
}
public enum Grade
{
    A, B, C, D, F
}
public class Enrollment
{
    public int EnrollmentID { get; set; }
    public int CourseID { get; set; }
    public int StudentID { get; set; }
    public Grade? Grade { get; set; }

    public virtual Course Course { get; set; }
    public virtual Student Student { get; set; }
}

public class Student
{
    public int ID { get; set; }
    public string LastName { get; set; }
    public string FirstMidName { get; set; }

    public string Address { get; set; }

    public string DOB { get; set; }
    public DateTime EnrollmentDate { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; }
}

public class Course
{
    public int CourseID { get; set; }
    public string Title { get; set; }
    public int Credits { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; }
}

public class MyContext : System.Data.Entity.DbContext
{
    public virtual DbSet<Course> Courses { get; set; }
    public virtual DbSet<Enrollment> Enrollments { get; set; }
    public virtual DbSet<Student> Students { get; set; }
}



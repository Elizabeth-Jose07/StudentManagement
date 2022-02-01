using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace StudentManagementSystem
{
    internal interface UserInterface
    {
        void showFirstScreen();
        void showStudentScreen();
        void showAdminScreen();
        void showStudentRegistrationScreen();
        void introduceNewCourseScreen();
        void showAllCoursesScreen();
        void showEnrollmentScreen();
        void showEnrollments();

    }



    class ExceedLimitException : Exception
    {
        public ExceedLimitException(string message) : base(message)
        {
        }
    }
    class AlreadySelectedCourse : Exception
    {
        public AlreadySelectedCourse(string message) : base(message)
        {
        }
    }
    class CourseException : Exception 
    {
        public CourseException(string message): base(message)
        {

        }
    }

    class ScreenDescription : UserInterface
    {
        //Presentation Layer
        Info info = new Info();
        Enroll en = new Enroll();

        public void showAdminScreen()
        {
            Console.WriteLine("\nYou are in admin screen\n");
            Console.WriteLine("\n\n------Welcome to Admin Dashboard------\n\n");
            Console.WriteLine("\nEnter your choice:\n1.Register a student on Student Management System\n2.Show all Student Details\n3.Show all current Student Enrollments\n" +
                "4.Introduce new course\n5.Show all courses\n6.Display Student Details by ID\n7.Enroll Student in a Course");
            int ch = 0;
            try
            {
                ch = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            switch (ch)
            {
                case 1:
                    showStudentRegistrationScreen();
                    break;
                case 2:
                    showAllStudentsScreen();
                    break;
                case 3:
                    showEnrollments();
                    break;

                case 4:
                    try { introduceNewCourseScreen(); }
                    catch (Exception e) { Console.WriteLine(e); }
                    break;
                case 5:
                    showAllCoursesScreen();
                    break;
                case 6:
                    showStudentDetailsById();
                    break;
                case 7:
                    try { showEnrollmentScreen(); }
                    catch (Exception e) { Console.Write(e); }

                    break;
                default:
                    Console.WriteLine("Please enter correct choice");
                    break;
            }
        }


        public void showEnrollments()
        {
            Console.WriteLine("\n\n----------All Enrollments screen----------\n\n");
            Console.WriteLine("Student Id\tStudent Name\tCourse Name\tDate of Enrollment\n");
            en.listOfEnrollments();
               
        }
        public void showStudentScreen()
        {

            Console.WriteLine("\n\n--------------Student screen----------\n\n");
            Console.WriteLine("\nEnter your choice:\n1.Register on Sudent Management System\n2.Register for a Course");
            int ch = Convert.ToInt32(Console.ReadLine());
            switch (ch)
            {
                case 1:
                    showStudentRegistrationScreen();
                    break;
                case 2:
                    try { showEnrollmentScreen(); }
                    catch (Exception e) { Console.Write(e); }
                    break;
                default:
                    Console.WriteLine("Please enter correct choice");
                    break;
            }

        }

        public void showAllStudentsScreen()
        {
            Console.WriteLine("\n----------------All Students Screen-------------\n");
            Console.WriteLine("\nAvailable Students\n");
            Console.WriteLine("Id\tName\tDate of Birth\n");

            foreach (Student student in en.listOfStudents())
                info.display(student);
        }

        public void showStudentDetailsById()
        {
            
            Student student = new Student();
            Console.WriteLine("\nenter student id");
            string id = Console.ReadLine();
            
            

            Console.WriteLine("\n Id\tName\tDate of Birth\n");
            Console.WriteLine(en.GetStudentById(id));

        }

        public void showStudentRegistrationScreen()
        {
            Console.WriteLine("\nYou are in student registration screen\n");
            Console.WriteLine("\n\n----------------------student screen-------------------------\n\n");
            Console.WriteLine("Enter student id:");
            string id = Console.ReadLine();
            Console.WriteLine("Enter student name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter date of birth");
            DateTime dateOfBirth = Convert.ToDateTime(Console.ReadLine());

            Student student = new Student(id, name, dateOfBirth.Date);

            en.register(student);

            //Console.WriteLine("Registration Successful");

        }

        public void showAllCoursesScreen()
        {
            Console.WriteLine("\n-----Available Courses-----\n");

            
            Console.WriteLine("\nSelect Type\n1. Degree\n2. Diploma\n");
            int ch = 0;
            try
            {
               ch = int.Parse(Console.ReadLine());
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
          


            switch (ch)
            {
                case 1:
                    var degreeCourse = en.listOfCourses();

                    Console.WriteLine("\nID\tCourse Name\tDuration\tFee\tSeats\tLevel\tPlacemment Available\n");

                    foreach (var course in degreeCourse)
                        Console.WriteLine(course);
                    break;
                case 2:

                    Console.WriteLine("\nID\tCourse Name\tDuration\tFee\tSeats\tType\n");

                    var diplomaCourse = en.listOfDCourses();
                    foreach (var course in diplomaCourse)
                        Console.WriteLine(course);
                    break;
                
                default:
                    Console.WriteLine("Wrong choice");
                    break;
            }
        }

        public void introduceNewCourseScreen()
        {
            Console.WriteLine("\n-----------You are in introduce new course screen----------\n");
            Console.WriteLine("\n\n---Add a new Course---\n\n");

  
            Console.WriteLine("Enter the course details to be added:");
            Console.WriteLine("Course ID:");
            string CourseId = Console.ReadLine();
            Console.WriteLine("Course Name:");
            string CourseName = Console.ReadLine();
            Console.WriteLine("Course Duration:");
            string CourseDuration = Console.ReadLine();
            Console.WriteLine("Course Fees:");
            float CourseFees = float.Parse(Console.ReadLine());
            Console.WriteLine("Enter Seats available:");
            int seats = Int32.Parse(Console.ReadLine());
           



            Console.WriteLine("Enter Degree/Diploma");
            string choice = Console.ReadLine();
            if (choice == "Degree")
            {
                Console.WriteLine("enter your degree type: Bachelors/Masters");
                string dtype = Console.ReadLine();
                Console.WriteLine("Is placement available(Yes/No)");
                String placement = Console.ReadLine().ToLower();
                if (placement == "yes")
                {
                    en.introduceDegree(new DegreeCourse(CourseId, CourseName, CourseDuration, CourseFees, seats, Enum.Parse<DegreeCourse.level>(dtype), true, "Degree"));
                }
                else if (placement == "no")
                {
                    en.introduceDegree(new DegreeCourse(CourseId, CourseName, CourseDuration, CourseFees, seats, Enum.Parse<DegreeCourse.level>(dtype), false, "Degree"));
                }
                Console.WriteLine("\nIntroduced a new course Successfully\n");
            }
            else if (choice == "Diploma")
            {
                Console.WriteLine("Course Type: professional/academic");
                string type = Console.ReadLine();
                en.introduceDiploma(new DiplomaCourse(CourseId, CourseName, CourseDuration, CourseFees, seats, Enum.Parse<DiplomaCourse.type>(type),false, "Diploma"));
                Console.WriteLine("\nIntroduced a new course Successfully\n");
            }


        }

        public void showEnrollmentScreen()
        {
            //Course course;
            Console.WriteLine("\n\n-----\nAll enrollments screen-----\n\n");
            Console.WriteLine("enter student id");
            int id = int.Parse(Console.ReadLine());

            Student student = en.GetStudentById(id.ToString());
            //en.listOfStudents();
            //bool isStudent = false;

            //foreach (var std in en.StudentArr)
            //{
            //    if (id == Int32.Parse(std.Id))
            //    {
            //        isStudent = true;
            //        student = std;
            //    }
            //}

            if (student==null)
            {
                Console.WriteLine("\nThe student is not registered on the Student Management System\n");
                return;
            }

            showAllCoursesScreen();
            Console.WriteLine("\nEnter course id");
            int courseid = int.Parse(Console.ReadLine());

            try
            {
                en.enroll(en.GetStudentById(id.ToString()), en.GetCourseById(courseid.ToString()), DateTime.Now);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }



            //foreach (var cs in en.CourseArr)
            //{
            //    if (courseid == Int32.Parse(cs.CourseId))
            //    {
            //        int courid = en.getId(id);
            //        if (courid == courseid)
            //        {
            //            throw new AlreadySelectedCourse("You have already selected the course");
            //        }
            //        else if (cs.SeatsAvaialble <= 0)
            //        {
            //            throw new ExceedLimitException("Seats not available");
            //        }

            //else
            //{
            //    course = cs;
            //    DateTime date1 = DateTime.Now;

            //    cs.SeatsAvaialble = cs.SeatsAvaialble - 1;
            //    Console.WriteLine("Registered for the course Successfully\n");
            //}
            //}



        }



        
        public void showFirstScreen()
        {

           // en.introduce(new DegreeCourse("11", "CS", "3 Months", 3000, 10, Enum.Parse<DegreeCourse.level>("Bachelors"),true, "Degree"));

            while (true)
            {
                Console.WriteLine("\n\n**********Welcome to SMS(Student Mgmt. System) v1.0**********\n\n");
                Console.WriteLine("Tell us who you are : \n1. Student\n2. Admin\n3. Exit\n");
                Console.WriteLine("Enter your choice ( 1, 2 or 3 ) : ");

                int op = 0;

                try
                {
                    op = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                switch (op)
                {
                    case 1:
                        showStudentScreen();
                        break;
                    case 2:
                        showAdminScreen();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Wrong Choice");
                        break;

                }
            }
        }
    }
}




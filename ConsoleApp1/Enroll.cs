using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace StudentManagementSystem
{
    public class SameCourse : Exception
    {
        public SameCourse(String message)
            : base(message)
        {

        }
    }
    public class StudentException : Exception
    {
        public StudentException(String message):base(message)
        {

        }
    }
    class Enroll : AppEngine
    {
        public Student student;
        public Course course;
        private DateTime enrollmentDate;

        public int count;
        public int coursecount;
        public int enrollmentcount;

        public List<Course> CourseArr = new List<Course>();
        public List<Student> StudentArr = new List<Student>();
        public List<Enroll> EnrollArr = new List<Enroll>();

        
       
         SqlConnection cnn = new SqlConnection(@"Data Source= ELZA-PC\SQLEXPRESS; initial catalog=studentmanagement;User id= sa; password =newuser123#");

        public Enroll()
        {
            count = 0;
            coursecount = 0;
            enrollmentcount = 0;
        }
        Enroll(Student student, Course course, DateTime enrollmentDate)
        {
            this.student = student;
            this.course = course;
            this.enrollmentDate = enrollmentDate;
        }

        public DateTime EnrollmentDate { get => enrollmentDate;  set => enrollmentDate = value;  }
        //{
        //    get { return enrollmentDate; }
        //    set { enrollmentDate = value; }
        //}
        public void introduce( Course course)
        {
            
        }


        public void introduceDegree(DegreeCourse course)
        {
            if (course.CourseId != "" && course.CourseName != "")
            {

                cnn.Open();

                SqlCommand cmd = new SqlCommand("insertCourse", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cid", course.CourseId);
                cmd.Parameters.AddWithValue("@cname", course.CourseName);
                cmd.Parameters.AddWithValue("@cdur", course.CourseDuration);
                // cmd.Parameters.AddWithValue("@fees", course.Fees);
                cmd.Parameters.AddWithValue("@ctype", course.CourseType);
                cmd.Parameters.AddWithValue("@seats", course.SeatsAvaialble);
                cmd.Parameters.AddWithValue("@clevel", course.courseLevel.ToString());
                cmd.Parameters.AddWithValue("@plc", course.isPlacementAvailable==true?"1":"0");
                cmd.Parameters.AddWithValue("@fees", course.Fees);

                if (cnn.State == System.Data.ConnectionState.Open)
                    cnn.Close();
                cnn.Open();
                try
                {

                    if (cmd.ExecuteNonQuery() > 0)
                    {

                        Console.WriteLine("Course{0} added successfully", course.CourseName);

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    cnn.Close();
                }
            }
            else throw new Exception(" enter comple details of the course");
        }

        public void introduceDiploma(DiplomaCourse course)
        {
            if (course.CourseId != "" && course.CourseName != "")
            {

                cnn.Open();

                SqlCommand cmd = new SqlCommand("insertCourse", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cid", course.CourseId);
                cmd.Parameters.AddWithValue("@cname", course.CourseName);
                cmd.Parameters.AddWithValue("@cdur", course.CourseDuration);
                // cmd.Parameters.AddWithValue("@fees", course.Fees);
                cmd.Parameters.AddWithValue("@ctype", course.CourseType);
                cmd.Parameters.AddWithValue("@seats", course.SeatsAvaialble);
                cmd.Parameters.AddWithValue("@clevel", course.csType.ToString());
                cmd.Parameters.AddWithValue("@plc", course.isPlacementAvailable == true ? "1" : "0");
                cmd.Parameters.AddWithValue("@fees", course.Fees);

                if (cnn.State == System.Data.ConnectionState.Open)
                    cnn.Close();
                cnn.Open();
                try
                {

                    if (cmd.ExecuteNonQuery() > 0)
                    {

                        Console.WriteLine("Course {0} added successfully", course.CourseName);

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    cnn.Close();
                }
            }
            else throw new Exception(" enter complete details of the course");
        }

        public List<Course> listOfCourses()
        {
            if (cnn.State == System.Data.ConnectionState.Open)
                cnn.Close();
            cnn.Open();
            CourseArr.Clear();
            SqlCommand cmd = new SqlCommand("SELECT * FROM course where [course level]='Degree'", cnn);
            SqlDataReader rd = cmd.ExecuteReader();
           // SqlDataAdapter da = new SqlDataAdapter();

            try
            {
                while (rd.Read())
                {
                   
                    DegreeCourse course = new DegreeCourse(rd[0].ToString(), rd[1].ToString(), rd[2].ToString(), float.Parse(rd[3].ToString()), int.Parse(rd[6].ToString()), Enum.Parse<DegreeCourse.level>(rd[7].ToString()), rd[5] as bool? ?? false, rd[4].ToString()) ;
                   
                    CourseArr.Add(course);
                    count++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            //return StudentArr;
            finally
            {
                cnn.Close();
            }

            if (CourseArr.Count != 0)
                return CourseArr;

            else
                throw new Exception("no course registered in system");
            // return CourseArr;
        }


        // reurns the course list  of diploma courses data retrived from data base
        public List<Course> listOfDCourses()
        {
            if (cnn.State == System.Data.ConnectionState.Open)
                cnn.Close();
            cnn.Open();
            CourseArr.Clear();
            SqlCommand cmd = new SqlCommand("SELECT * FROM course where [course level]='Diploma'", cnn);
            SqlDataReader rd = cmd.ExecuteReader();
            // SqlDataAdapter da = new SqlDataAdapter();

            try
            {
                while (rd.Read())
                {

                    DiplomaCourse course = new DiplomaCourse(rd[0].ToString(), rd[1].ToString(), rd[2].ToString(), float.Parse(rd[3].ToString()), int.Parse(rd[6].ToString()), Enum.Parse<DiplomaCourse.type>(rd[7].ToString()), rd[5] as bool? ?? false, rd[4].ToString());

                    CourseArr.Add(course);
                    count++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            //return StudentArr;
            finally
            {
                cnn.Close();
            }

            if (CourseArr.Count != 0)
                return CourseArr;

            else
                throw new Exception("no course registered in system");
            // return CourseArr;
        }


        //register a student to  student management system
        public void register(Student student)
        {
            //StudentArr.Add(student);
            //count++;
            if (student.Id != "" && student.Name != "" )
            {

                if (cnn.State == System.Data.ConnectionState.Open)
                    cnn.Close();
                cnn.Open();
                SqlCommand cmd = new SqlCommand("procinsertstudents",cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@stid", student.Id);
                cmd.Parameters.AddWithValue("stname", student.Name);
                cmd.Parameters.AddWithValue("stdob", student.DOB);

                try
                {
                   
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        
                        Console.WriteLine("student {0} added successfully", student.Name);
                        
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    cnn.Close();
                }
            }
            else throw new StudentException(" enter comple details of the student");


        }
        //returns the student list , data retrieved from the database
        public List<Student> listOfStudents()
        {
            if (cnn.State == System.Data.ConnectionState.Open)
                cnn.Close();
            cnn.Open();
            StudentArr.Clear();
            SqlCommand cmd = new SqlCommand("SELECT * FROM STUDENTS",cnn);
            SqlDataReader rd = cmd.ExecuteReader();
            
            try
            {
                while(rd.Read())
                {
                    Student student = new Student(rd[0].ToString(), rd[1].ToString(), (DateTime)rd[2]);
                    StudentArr.Add(student);
                    count++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //return StudentArr;
            finally
            {
                cnn.Close();
            }

            if (StudentArr.Count != 0)
                return StudentArr;

            else
                throw new Exception("no students registered in system");

        }

        //Enroll a student to a course and store it to data base
        public void enroll(Student student, Course course, DateTime enrollmentdate)
        {
            if (student.Id != "" && course.CourseId != "")
            {
                if (cnn.State == System.Data.ConnectionState.Open)
                    cnn.Close();
                cnn.Open();
                
                
                try
                {

                    SqlCommand cmd = new SqlCommand("procenroll", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@stid", student.Id);
                    cmd.Parameters.AddWithValue("@cid", course.CourseId);
                    cmd.Parameters.AddWithValue("@doe", DateTime.UtcNow);


                    if (cmd.ExecuteNonQuery() > 0)
                    {

                        Console.WriteLine("student {0} enrolled successfully", student.Name);
                        
                    }
                }
               
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


               
                finally
                {
                   
                    cnn.Close();
                }

            }
            else throw new Exception(" enter complete details ");


        }


        //EnrollArr.Add(new Enroll(student, course, enrollmentdate));
        //enrollmentcount++;
    

        //returns the enroll list, data retreived from database
        public  void  listOfEnrollments()
        {
            if (cnn.State == System.Data.ConnectionState.Open)
                cnn.Close();
            cnn.Open();

            SqlCommand cmd = new SqlCommand("GetEnrollments", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            SqlDataReader rd = cmd.ExecuteReader();
            try
            {
                while(rd.Read())
                {
                
                    //Enroll enroll = new Enroll(GetStudentById(rd[0].ToString()), GetCourseById(rd[1].ToString()), (DateTime) rd[2]);
                    //EnrollArr.Add(enroll);
                    Console.WriteLine(rd[0]+"\t"+rd[1]+"\t"+rd[2]+"\t"+rd[3]);
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                cnn.Close();
            }

            //if (EnrollArr.Count != 0)
            //    return EnrollArr;

            //else
            //    throw new Exception("no students enrolled for courses");

            //return EnrollArr;
        }

        //gets students by id
        public Student GetStudentById(string id)
        {
            if (cnn.State == System.Data.ConnectionState.Open)
                    cnn.Close();
            cnn.Open();

            SqlCommand cmd = new SqlCommand("procGetStudentByID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters .AddWithValue("@stid", id);
            SqlDataReader rd = cmd.ExecuteReader();
            
            
                if (rd.Read())
                {
                Student student = new Student(rd[0].ToString(), rd[1].ToString(), (DateTime)rd[2]);
                cnn.Close();

                return student;
                }
            
            
            else
            {
                throw new StudentException(" student not registered in system");
            }

            


        }
        //gets the course by its id
        public Course GetCourseById(string id)
        {
            if (cnn.State == System.Data.ConnectionState.Open)
                cnn.Close();
            cnn.Open();

            SqlCommand cmd = new SqlCommand("procGetCourseByID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@cid", id);
            SqlDataReader rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                Course course = new Course(rd[0].ToString(), rd[1].ToString());
                cnn.Close();
                return course;
            }
            else
            {
                throw new Exception(" course doesn't exist");
            }
            



        }


        //old code for getting students by id
        //public int getId(int id)
        //{
        //    int ccid = (from en in EnrollArr
        //                where int.Parse(en.student.Id) == id
        //                select int.Parse(en.course.CourseId)).SingleOrDefault();
        //    return ccid;
        //}
        public override string ToString()
        {
            return string.Format("\n" + student.Id + "\t\t" + student.Name + "\t\t" + course.CourseName + "\t\t" + EnrollmentDate.ToShortDateString() + "\n");
        }

    }

}

using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem
{
    class Info
    {
        public void display(Student student)
        {
            //test by elizabeth


            Console.WriteLine(student);


        }
        public void display(Course course)
        {
            //Code here to display the details of given course
            Console.WriteLine(course);

        }

        public void display(Enroll enroll)
        {
            Console.WriteLine(enroll);
        }

    }
}

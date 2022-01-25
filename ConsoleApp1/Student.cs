using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem
{
    //model layer
    public class Student
    {
        string id;
        string name;
        DateTime dateOfBirth;

        public Student() { }
        public Student(string id, string name, DateTime? dateOfBirth)
        {
            id = id ?? "S000";
            this.id = id;
            name = name ?? "JK0";
            this.name = name;
            dateOfBirth = dateOfBirth ?? DateTime.Today;
            this.dateOfBirth = (DateTime)dateOfBirth;
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public DateTime DOB
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        public override string ToString()
        {


            return "\n" + Id + "\t" + Name + "\t" + DOB.ToShortDateString() + " ";
        }
    }




}


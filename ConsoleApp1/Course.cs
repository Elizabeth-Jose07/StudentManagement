using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem
{
     class Course
    {
        protected string id;
        protected string name;
        protected string duration;
        protected float fees;
        protected int seatsavaialble;
        protected string courseType;

        public Course() { }
        public Course(string id,string name)
        {
            this.id = id;
            this.name = name;
        }
        public Course(string id, string name, string duration, float fees, int seatsavaialble, string courseType)
        {
            this.id = id;
            this.name = name;
            this.duration = duration;
            this.fees = fees;
            this.seatsavaialble = seatsavaialble;
            this.courseType = courseType;
        }

        public string CourseId
        {
            get { return id; }
            set { id = value; }
        }

        public string CourseName
        {
            get { return name; }
            set { name = value; }
        }

        public string CourseDuration
        {
            get { return duration; }
            set { duration = value; }
        }

        public int SeatsAvaialble
        {
            get { return seatsavaialble; }
            set { seatsavaialble = value; }
        }

        public float Fees
        {
            get { return fees; }
            set { fees = value; }
        }

        public string CourseType
        {
            get { return courseType; }
            set { courseType = value; }
        }


        

    }

    class DegreeCourse : Course
    {
        public DegreeCourse() { }
        public enum level
        {
            Bachelors,
            Masters
        }
        public level courseLevel;
        public bool active;
        public bool isPlacementAvailable;
        public DegreeCourse(string id, string name, string duration, float fees, int seatsavaialble, level courseLevel, bool isPlacementAvailable, string courseType) : base(id, name, duration, fees, seatsavaialble, courseType)
        {
            this.courseLevel = courseLevel;
            this.active = active;
            this.isPlacementAvailable = isPlacementAvailable;
            calculateMonthlyFee();
        }

        public  float  calculateMonthlyFee()
        {
            if (isPlacementAvailable)
            {
                this.Fees = this.Fees + ((this.Fees * 10) / 100);
            }
            else if (!isPlacementAvailable)
            {
                this.Fees = this.Fees;
            }
            return Fees;
        }

        public override string ToString()
        {
            return $"\n {CourseId}\t{CourseName}\t\t{CourseDuration}\t{Fees}\t{SeatsAvaialble}\t{courseLevel}\t{ (isPlacementAvailable == true ? "Yes" : "No")}\n";
        }

    }
    class DiplomaCourse : Course
    {
        public DiplomaCourse() { }
        public bool isPlacementAvailable;
        public enum type
        {
            professional,
            academic
        }
        public type csType;
        public DiplomaCourse(string id, string name, string duration, float fees, int seats, type csType,bool isPlacementAvailable, string courseType) : base(id, name, duration, fees, seats, courseType)
        {
            this.csType = csType;
            this.isPlacementAvailable = isPlacementAvailable;
           calculateMonthlyFee();
        }

        public float calculateMonthlyFee()
        {
            if (this.csType == type.professional)
            {
                this.Fees = this.Fees + ((this.Fees * 10) / 100);
            }
            else
            {
                this.Fees = this.Fees + ((this.Fees * 5) / 100);
            }
            return Fees;
            
        }

        public override string ToString()
        {

            return string.Format("\n" + CourseId + "\t" + CourseName + "\t\t" + CourseDuration + "\t" + Fees + "\t" + SeatsAvaialble + "\t" + csType + "\n");
        }

    }

}
using System;
using System.Collections.Generic;
using System.Text;

namespace ExaminationSystem
{
    internal class Student
    {
        public string Name { get; set;}
        public int ID { get; set; }

        public Student(string? name, int id)
        {
            if (name == null) name = "null Name";
            Name = name;
            ID = id;
        }
        //public void OnExamStarted(object sender, ExamEventArgs e) { }
    }
}

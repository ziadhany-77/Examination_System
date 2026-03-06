using System;
using System.Collections.Generic;
using System.Text;

namespace ExaminationSystem
{
    internal class Subject
    {
        public string Name { get; set; }
        Student[] _enrolledStudents;

        public Student[] Students {get { return (Student[])_enrolledStudents.Clone(); }}

        public Subject(string? name, int maxStudents)
        {
            if (name == null) name = "corrupted Name";
            Name = name;
            _enrolledStudents = new Student[maxStudents];
        }

        public void EnrollStudent(Student? student)
        {
            if (student == null) return;
            for (int i = 0; i < _enrolledStudents.Length; i++)
            {
                if (student.ID == _enrolledStudents[i].ID) return;
                if (_enrolledStudents[i] == null)
                {
                    _enrolledStudents[i] = student;
                    return;
                }
            }
            throw new InvalidOperationException("Subject is full. Cannot enroll more students.");
        }
    }
}

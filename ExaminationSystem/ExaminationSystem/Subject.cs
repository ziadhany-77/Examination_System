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
                if (_enrolledStudents[i] == null)           // empty slot → enroll here
                {
                    _enrolledStudents[i] = student;
                    return;
                }
                if (_enrolledStudents[i].ID == student.ID)  // already enrolled → skip
                    return;
            }
            throw new InvalidOperationException("Subject is full.");
        }
    }
}

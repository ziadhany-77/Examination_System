using ExaminationSystem.Answers;
using ExaminationSystem.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExaminationSystem.Exams
{
    internal abstract class Exam : ICloneable, IComparable<Exam>
    {
        public int TimeInMins { get; set { field = value > 5 ? 5 : value; } }
        public int NumberOfQuestions { get; set; }
        Subject Subject { get; set; }
        ExamMode Mode { get; set; }

        Question[] _questions;

        Dictionary<Question, Answer> _questionAnswerDictionary;

        public Question[] questions { get { return (Question[])_questions.Clone(); } }

        public Dictionary<Question, Answer> QuestionAnswerDictionary
        {
            get { return new Dictionary<Question, Answer>(_questionAnswerDictionary); }
        }

        protected Exam(int timeInMins, int numberOfQuestions, Subject subject)
        {
            TimeInMins = timeInMins;
            NumberOfQuestions = numberOfQuestions;
            Subject = subject;
            Mode = ExamMode.Queued;
            _questions = new Question[numberOfQuestions];
            _questionAnswerDictionary = new Dictionary<Question, Answer>();
        }
        public abstract void ShowExam();
        public abstract void Start();
        public abstract void Finish();
        public abstract void CorrectExam();

        public int CompareTo(Exam? other)
        {
            if (other == null) return 1;
            if (this == other) return 0;
            if (this.TimeInMins > other.TimeInMins) return 1;
            if (this.TimeInMins < other.TimeInMins) return -1;
            else
            {
                if (this.NumberOfQuestions > other.NumberOfQuestions) return 1;
                if (this.NumberOfQuestions < other.NumberOfQuestions) return -1;
                return 0;
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        override public bool Equals(object? obj)
        {
            if (obj is Exam otherExam)
            {
                return TimeInMins == otherExam.TimeInMins &&
                       NumberOfQuestions == otherExam.NumberOfQuestions &&
                       Subject.Equals(otherExam.Subject) &&
                       Mode == otherExam.Mode;
            }
            return false;
        }
        override public int GetHashCode()
        {
            return HashCode.Combine(TimeInMins, NumberOfQuestions, Subject, Mode);
        }


        override public string ToString()
        { 
            return $"Exam Time: {TimeInMins} mins, Number of Questions: {NumberOfQuestions}, Subject: {Subject.Name}, Mode: {Mode}";
        }   
    }

    
}

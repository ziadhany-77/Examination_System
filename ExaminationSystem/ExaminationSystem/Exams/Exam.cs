using ExaminationSystem.Answers;
using ExaminationSystem.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExaminationSystem.Exams
{
    internal abstract class Exam : ICloneable, IComparable<Exam>
    {
        
        public int TimeInMins { get; set { field = value > 5 ? value : 5; } }
        public int NumberOfQuestions { get; set { field = value > 0 ? value : 5; } }
        public Subject Subject { get; set; }
        public ExamMode Mode { get; set; }

        QuestionList _questions;

        Dictionary<Question, Answer[]> _questionAnswerDictionary;

        public QuestionList Questions { get { return _questions; } }

        public Dictionary<Question, Answer[]> QuestionAnswerDictionary
        {
            get { return new Dictionary<Question, Answer[]>(_questionAnswerDictionary); }
        }

        protected Exam(int timeInMins, int numberOfQuestions, Subject subject, int version)
        {
            TimeInMins = timeInMins;
            NumberOfQuestions = numberOfQuestions;
            Subject = subject;
            Mode = ExamMode.Queued;
            _questions = new QuestionList($"{subject.Name}_v{version}");
            _questionAnswerDictionary = new Dictionary<Question, Answer[]>();
        }
        public abstract void ShowExam();
        public virtual void Start() 
        {
            Mode = ExamMode.Starting;
            ShowExam();
            foreach (Question q in _questions)
            {
                if (q is ChooseAllQuestion)
                {
                    
                    List<Answer> selected = new List<Answer>();
                    Console.WriteLine("Enter answer IDs one at a time. Type 0 when done:");

                    while (true)
                    {
                        if (!int.TryParse(Console.ReadLine(), out int inputId))
                        {
                            Console.WriteLine("Invalid input. Please enter a numeric ID:");
                            continue;
                        }

                        if (inputId == 0)
                        {
                            if (selected.Count == 0)
                            {
                                Console.WriteLine("You must select at least one answer.");
                                continue;
                            }
                            break;
                        }

                        Answer? picked = q.Answers.GetAnswerByID(inputId);
                        if (picked == null)
                        {
                            Console.WriteLine("Answer ID not found. Try again:");
                            continue;
                        }

                        if (selected.Contains(picked))
                        {
                            Console.WriteLine("Already selected. Pick a different answer:");
                            continue;
                        }

                        selected.Add(picked);
                        Console.WriteLine($"Added: {picked.Text}. Enter another ID or 0 to finish:");
                    }

                    _questionAnswerDictionary.Add(q, selected.ToArray());
                }
                else
                {
                    
                    Answer? studentAnswer = null;

                    while (studentAnswer == null)
                    {
                        Console.WriteLine("Enter your answer ID:");

                        if (!int.TryParse(Console.ReadLine(), out int inputId))
                        {
                            Console.WriteLine("Invalid input. Please enter a numeric ID:");
                            continue;
                        }

                        studentAnswer = q.Answers.GetAnswerByID(inputId);

                        if (studentAnswer == null)
                            Console.WriteLine("Answer ID not found. Try again:");
                    }

                    _questionAnswerDictionary.Add(q, new Answer[] { studentAnswer });
                }
            }
            Finish();

        }
        
        public virtual void Finish() { }
        public virtual int CorrectExam() 
        {
            if (_questionAnswerDictionary.Count == 0)
                throw new InvalidOperationException("Exam has not been started yet.");
            int totalMarks = 0;
            foreach (var item in _questionAnswerDictionary)
            {
                if (item.Key.CheckAnswer(item.Value))
                {
                    totalMarks += item.Key.Marks;
                }
            }
            return totalMarks;
        }

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

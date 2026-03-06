using ExaminationSystem.Answers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExaminationSystem.Questions
{
    internal abstract class Question
    {
        public string Header { get; set; }
        public string Body { get; set; }
        //public int Marks { get;  set { field = value > 0 ? value : 1; } }
        public Answer CorrectAnswer { get; set; }
        public AnswersList Answers { get; set; }

        int _marks;

        public int Marks
        {
            get { return _marks; }
            set { _marks = value > 0 ? value : 1; }
        }

        protected Question(string? header, string body, int marks, Answer? correctAnswer, int answerSize)
        {
            if (correctAnswer == null)
                throw new ArgumentNullException(nameof(correctAnswer), "Correct answer cannot be null.");
            if (string.IsNullOrEmpty(header)) header = "corrupted Header";
            if (string.IsNullOrEmpty(body)) body = "corrupted Body";
            Body = body;
            Marks = marks;
            Header = header;
            CorrectAnswer = correctAnswer;
            Answers = new AnswersList(answerSize);
        }

        public abstract void Display();
        public abstract bool CheckAnswer(Answer[]? studentAnswer);
        public override string ToString()
        {
            string AllAnswers = "";
            foreach (Answer answer in Answers.ListOfAnswers) AllAnswers += answer.ToString();
            return $"Question Header: {Header}, Question Body: {Body}, Marks: {Marks},  Answers: {AllAnswers}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Question otherQuestion)
            {
                return Header == otherQuestion.Header &&
                       Body == otherQuestion.Body &&
                       Marks == otherQuestion.Marks &&
                       CorrectAnswer.Equals(otherQuestion.CorrectAnswer);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Header, Body, Marks, CorrectAnswer);
        }
    }

    class TrueFalseQuestion : Question
    {
        public TrueFalseQuestion(string? header, string body, int marks, Answer? correctAnswer) :
            base(header, body, marks, correctAnswer, 2)
        { }

        public override bool CheckAnswer(Answer[]? studentAnswer)
        {
            if (studentAnswer == null || studentAnswer.Length == 0) return false;
            return this.CorrectAnswer.Equals(studentAnswer[0]);
        }

        public override void Display()
        {
            Console.WriteLine($"True/False Question: {Header}\n{Body}\nMarks: {Marks}\nPossible Answers: {Answers[0]}, {Answers[1]}");
        }
    }

    class ChooseOneQuestion : Question
    {

        public ChooseOneQuestion(string? header, string body, int marks, Answer? correctAnswer) : base(header, body, marks, correctAnswer, 4)
        { }
        public override bool CheckAnswer(Answer[]? studentAnswer)
        {
            return this.CorrectAnswer.Equals(studentAnswer?[0]);
        }
        public override void Display()
        {
            Console.WriteLine($"Multiple Choice Question: {Header}\n{Body}\nMarks: {Marks}\nPossible Answers:");
            for (int i = 0; i < Answers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Answers[i]}");
            }
        }
    }

    class ChooseAllQuestion : Question
    {
        Answer[] _correctAnswers;

        public ChooseAllQuestion(string? header, string body, int marks, Answer[] correctAnswers) 
            : base(header, body, marks,null, 5)
        {
            _correctAnswers = correctAnswers ?? throw new ArgumentNullException(nameof(correctAnswers)); ;
        }
        public override bool CheckAnswer(Answer[]? studentAnswer)
        {
            throw new NotImplementedException();
        }

        public override void Display()
        {
            Console.WriteLine($"Multiple Choice Question: {Header}\n{Body}\nMarks: {Marks}\nPossible Answers:");
            for (int i = 0; i < Answers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Answers[i]}");
            }
        }
    }
}
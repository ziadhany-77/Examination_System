using ExaminationSystem.Answers;
using ExaminationSystem.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExaminationSystem.Exams
{
    internal class FinalExam : Exam
    {
        public FinalExam(int timeInMins, int numberOfQuestions, Subject subject, int version)
            : base(timeInMins, numberOfQuestions, subject, version)
        { }

        public override void ShowExam()
        {
            Console.WriteLine("=======================================");
            Console.WriteLine("             FINAL EXAM                ");
            Console.WriteLine("=======================================");
            Console.WriteLine(ToString());
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Questions:");
            foreach (Question q in Questions)
            {
                q.Display();
            }
            Console.WriteLine("=======================================\n");
        }

        public override void Finish()
        {
            Mode = ExamMode.Finished;
            CorrectExam(); // calculate internally — but never reveal

            Console.WriteLine("\n=======================================");
            Console.WriteLine("        YOUR SUBMITTED ANSWERS         ");
            Console.WriteLine("=======================================");

            foreach (var item in QuestionAnswerDictionary)
            {
                Question q = item.Key;
                Answer[] studentAnswers = item.Value;

                Console.WriteLine($"\nQ: {q.Body}");

                Console.Write("Your Answer(s): ");
                foreach (Answer a in studentAnswers)
                    Console.Write($"{a.Text}  ");
                Console.WriteLine();
            }

            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Your answers have been submitted.");
            Console.WriteLine("Results will be announced later.");
            Console.WriteLine("=======================================\n");
        }
    }
}

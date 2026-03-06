using System;
using System.Collections.Generic;
using System.Text;

namespace ExaminationSystem.Answers
{
    internal class AnswersList
    {
        Answer[] AnswersArray;
        public Answer[] ListOfAnswers
        {
            get { return (Answer[])AnswersArray.Clone(); }
        }

        int _count = 0;
        public int Count{ get { return _count;}}
        int Size { get; set; }

        public AnswersList(int size)
        {
            Size = size;
            AnswersArray = new Answer[size];
        }


        public void AddAnswer(Answer? answer)
        {
            if (answer == null) return;

            if (_count == Size)
            {
                throw new InvalidOperationException("Answers list is full.");
            }
            else
            {
                AnswersArray.SetValue(answer, Count);
                _count++;
            }
        }

        public Answer? GetAnswerByID(int id)
        {
            foreach (Answer answer in AnswersArray)
            {
                if (answer.ID == id) return answer;
            }
            return null;
        }


        public Answer this[int index]
        {
            get 
            { 
                if(index >= 0 && index < _count) return AnswersArray[index]; 
                throw new IndexOutOfRangeException("Index is out of range");
            }
        }

    }
}

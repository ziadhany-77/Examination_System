using System;
using System.Collections.Generic;
using System.Text;

namespace ExaminationSystem.Answers
{
    internal class Answer: IComparable<Answer>
    {
        public int ID { get; init; }
        public string Text { get; set; }

        public Answer(string? text, int id) 
        {
            if (string.IsNullOrEmpty(text)) text = "corrupted Text";
            ID = id;
            Text = text;
        }

        public override string ToString()
        {
            return $"Answer ID: {ID}, Answer Text: {Text}";
        }

        override public bool Equals(object? obj)
        {
            if (obj is Answer otherAnswer)
            {
                return this.ID == otherAnswer.ID || this.Text == otherAnswer.Text;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Text);
        }
        public int CompareTo(Answer? other)
        {
            if (other == null) return 1;

            if (ReferenceEquals(this, other)) return 0; 

            if(this.ID < other.ID) return -1;
            else if(this.ID > other.ID) return 1;
            else return 0;  
        }
    }
}

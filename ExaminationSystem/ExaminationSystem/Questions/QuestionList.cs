using System;
using System.Collections.Generic;
using System.Text;

namespace ExaminationSystem.Questions
{
    internal class QuestionList: List<Question>
    {
        string _logFile;
        public string LogFile
        {
            get { return _logFile; }
            init { _logFile = value; }
        }
        public QuestionList(string logFile)
        {
            LogFile = @$"D:\{logFile}.txt";
                
        }


        public new void Add(Question question)
        {
            base.Add(question);
            string fileName = LogFile;
            FileStream stream = null;
            try
            {
                // Create a FileStream with mode CreateNew
                stream = new FileStream(fileName, FileMode.Append);
                // Create a StreamWriter from FileStream
                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    writer.WriteLine(question);
                    writer.WriteLine("==================");
                }
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
            }

        }
    }
}

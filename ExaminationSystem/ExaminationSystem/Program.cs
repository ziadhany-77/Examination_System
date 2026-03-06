using ExaminationSystem.Answers;
using ExaminationSystem.Questions;

namespace ExaminationSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("       EXAMINATION SYSTEM TEST          ");
            Console.WriteLine("========================================\n");

            
            #region test-answers
            Console.WriteLine("--- 1. Answer Tests ---");

            Answer ansTrue = new Answer("True", 1);
            Answer ansFalse = new Answer("False", 2);
            Answer ansA = new Answer("Paris",3);
            Answer ansB = new Answer("London",4 );
            Answer ansC = new Answer("Berlin",5 );
            Answer ansD = new Answer("Rome",6);
            Answer ansAll1 = new Answer("Photosynthesis",7);
            Answer ansAll2 = new Answer("Respiration",8);
            Answer ansAll3 = new Answer("Transpiration",9);

            Console.WriteLine(ansTrue);
            Console.WriteLine(ansFalse);
            Console.WriteLine(ansAll1);
            Console.WriteLine($"ansTrue.Equals(ansTrue): {ansTrue.Equals(ansTrue)}");   // true
            Console.WriteLine($"ansTrue.Equals(ansFalse): {ansTrue.Equals(ansFalse)}"); // false
            Console.WriteLine($"CompareTo: {ansTrue.CompareTo(ansFalse)}\n");
            #endregion

            #region test-answerslist
            Console.WriteLine("--- 2. AnswersList Tests ---");

            AnswersList list = new AnswersList(3);
            list.AddAnswer(ansA);
            list.AddAnswer(ansB);
            list.AddAnswer(ansC);

            Console.WriteLine($"Count: {list.Count}");           // 3
            Console.WriteLine($"Index[0]: {list[0]}");          // Paris
            Console.WriteLine($"Index[1]: {list[1]}");          // London
            Console.WriteLine($"GetByID: {list.GetAnswerByID(ansB.ID)}"); // London

            // Test full list exception
            try
            {
                list.AddAnswer(ansD); // should throw
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Expected Exception: {ex.Message}");
            }

            // Test null add (silent skip)
            list.AddAnswer(null);
            Console.WriteLine($"Count after null add: {list.Count}\n"); // still 3
            #endregion

            #region test-truefalse
            Console.WriteLine("--- 3. TrueFalseQuestion Tests ---");

            TrueFalseQuestion tfQ = new TrueFalseQuestion(
                "Biology Q1",
                "Plants produce oxygen through photosynthesis.",
                5,
                ansTrue
            );
            tfQ.Answers.AddAnswer(ansTrue);
            tfQ.Answers.AddAnswer(ansFalse);

            tfQ.Display();

            Console.WriteLine($"Correct answer (True):  {tfQ.CheckAnswer(new Answer[] { ansTrue })}");   // true
            Console.WriteLine($"Wrong answer (False):   {tfQ.CheckAnswer(new Answer[] { ansFalse })}");  // false
            Console.WriteLine($"Null answer:            {tfQ.CheckAnswer(null)}");                        // false
            Console.WriteLine($"Empty array:            {tfQ.CheckAnswer(new Answer[] { })}\n");          // false
            #endregion

            #region test-chooseone
            Console.WriteLine("--- 4. ChooseOneQuestion Tests ---");

            ChooseOneQuestion chooseOneQ = new ChooseOneQuestion(
                "Geography Q1",
                "What is the capital of France?",
                10,
                ansA  // Paris is correct
            );
            chooseOneQ.Answers.AddAnswer(ansA);
            chooseOneQ.Answers.AddAnswer(ansB);
            chooseOneQ.Answers.AddAnswer(ansC);
            chooseOneQ.Answers.AddAnswer(ansD);

            //chooseOneQ.Display();
            Console.WriteLine(chooseOneQ);
            Console.WriteLine(tfQ);
            Console.WriteLine($"Correct answer (Paris):  {chooseOneQ.CheckAnswer(new Answer[] { ansA })}"); // true
            Console.WriteLine($"Wrong answer (London):   {chooseOneQ.CheckAnswer(new Answer[] { ansB })}"); // false
            Console.WriteLine();
            #endregion

            #region test-ChooseAllQuestion

            //Console.WriteLine("--- 5. ChooseAllQuestion Tests ---");

            //Answer[] correctSet = new Answer[] { ansAll1, ansAll2, ansAll3 };

            //ChooseAllQuestion chooseAllQ = new ChooseAllQuestion(
            //    "Biology Q2",
            //    "Which of the following are plant processes?",
            //    15,
            //    correctSet
            //);
            //chooseAllQ.Answers.AddAnswer(ansAll1);
            //chooseAllQ.Answers.AddAnswer(ansAll2);
            //chooseAllQ.Answers.AddAnswer(ansAll3);
            //chooseAllQ.Answers.AddAnswer(new Answer("Digestion"));
            //chooseAllQ.Answers.AddAnswer(new Answer("Circulation"));

            //chooseAllQ.Display();

            //// All correct (different order — set comparison)
            //Answer[] studentCorrect = new Answer[] { ansAll2, ansAll3, ansAll1 };
            //Console.WriteLine($"All correct (shuffled): {chooseAllQ.CheckAnswer(studentCorrect)}"); // true

            //// Missing one answer
            //Answer[] studentPartial = new Answer[] { ansAll1, ansAll2 };
            //Console.WriteLine($"Partial answer:         {chooseAllQ.CheckAnswer(studentPartial)}"); // false

            //// Null
            //Console.WriteLine($"Null answers:           {chooseAllQ.CheckAnswer(null)}"); // false
            //Console.WriteLine();

            #endregion

            #region test-polymorphism
            Console.WriteLine("--- 6. Runtime Polymorphism ---");

            Question[] exam = new Question[] { tfQ, chooseOneQ };

            foreach (Question q in exam)
            {
                Console.WriteLine($"\n[{q.GetType().Name}]");
                q.Display(); // polymorphic call
            }
            #endregion

            #region test-edgecases
            Console.WriteLine("\n--- 8. Edge Cases ---");

            // Null header → corrupted fallback
            TrueFalseQuestion nullHeaderQ = new TrueFalseQuestion(null, "Body text", 3, ansTrue);
            Console.WriteLine($"Null header fallback: '{nullHeaderQ.Header}'"); // "corrupted Header"

            // Marks <= 0 → defaults to 1
            TrueFalseQuestion zeroMarksQ = new TrueFalseQuestion("H", "B", -5, ansTrue);
            Console.WriteLine($"Negative marks fallback: {zeroMarksQ.Marks}"); // 1

            // Null correctAnswer → exception
            try
            {
                TrueFalseQuestion badQ = new TrueFalseQuestion("H", "B", 5, null);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Expected Exception: {ex.ParamName} cannot be null");
            }

            // Out of range indexer
            try
            {
                var _ = tfQ.Answers[99];
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"Expected Exception: {ex.Message}");
            }
            #endregion

            QuestionList listquestions = new("test");
            listquestions.Add(tfQ);
            listquestions.Add(chooseOneQ);
        }
    }
}

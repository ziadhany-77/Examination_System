using ExaminationSystem.Answers;
using ExaminationSystem.Exams;
using ExaminationSystem.Questions;

namespace ExaminationSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {


            

            // =============================================
            //         EXAMINATION SYSTEM - MAIN
            // =============================================

            // ── 1. Create Subject ─────────────────────────
            Subject mathSubject = new Subject("Mathematics", 30);

            // ── 2. Create Students and Enroll ─────────────
            Student student1 = new Student("Alice Johnson", 1001);
            Student student2 = new Student("Bob Smith", 1002);
            Student student3 = new Student("Carol White", 1003);

            mathSubject.EnrollStudent(student1);
            mathSubject.EnrollStudent(student2);
            mathSubject.EnrollStudent(student3);

            Console.WriteLine("=======================================");
            Console.WriteLine("      EXAMINATION SYSTEM LOADED        ");
            Console.WriteLine("=======================================");
            Console.WriteLine($"Subject : {mathSubject.Name}");
            Console.WriteLine($"Students enrolled: {mathSubject.Students.Length}");
            foreach (Student s in mathSubject.Students)
                if (s != null) Console.WriteLine($"  - [{s.ID}] {s.Name}");
            Console.WriteLine();

            // ── 3. Create Answers ──────────────────────────

            // True/False answers
            Answer ansTrue = new Answer("True", 1);
            Answer ansFalse = new Answer("False", 2);

            // Choose One answers
            Answer ansA = new Answer("2", 10);
            Answer ansB = new Answer("3", 11);
            Answer ansC = new Answer("4", 12);
            Answer ansD = new Answer("5", 13);

            // Choose All answers
            Answer ansPrime1 = new Answer("2", 20);
            Answer ansPrime2 = new Answer("3", 21);
            Answer ansPrime3 = new Answer("5", 22);
            Answer ansNotP1 = new Answer("4", 23);
            Answer ansNotP2 = new Answer("6", 24);

            // ── 4. Create Questions ────────────────────────

            // Q1 — True/False
            TrueFalseQuestion q1 = new TrueFalseQuestion(
                "Basic Arithmetic",
                "Is 2 + 2 equal to 4?",
                5,
                ansTrue
            );
            q1.Answers.AddAnswer(ansTrue);
            q1.Answers.AddAnswer(ansFalse);

            // Q2 — Choose One
            ChooseOneQuestion q2 = new ChooseOneQuestion(
                "Multiplication",
                "What is 2 x 2?",
                10,
                ansC   // correct = "4"
            );
            q2.Answers.AddAnswer(ansA);  // 2
            q2.Answers.AddAnswer(ansB);  // 3
            q2.Answers.AddAnswer(ansC);  // 4 ← correct
            q2.Answers.AddAnswer(ansD);  // 5

            // Q3 — Choose All
            ChooseAllQuestion q3 = new ChooseAllQuestion(
                "Prime Numbers",
                "Select ALL prime numbers from the list:",
                15,
                new Answer[] { ansPrime1, ansPrime2, ansPrime3 }  // 2, 3, 5
            );
            q3.Answers.AddAnswer(ansPrime1);  // 2
            q3.Answers.AddAnswer(ansPrime2);  // 3
            q3.Answers.AddAnswer(ansPrime3);  // 5
            q3.Answers.AddAnswer(ansNotP1);   // 4
            q3.Answers.AddAnswer(ansNotP2);   // 6

            // ── 5. Create Exams and add questions ─────────
            PracticeExam practiceExam = new PracticeExam(30, 3, mathSubject, 1);
            practiceExam.Questions.Add(q1);
            practiceExam.Questions.Add(q2);
            practiceExam.Questions.Add(q3);

            FinalExam finalExam = new FinalExam(60, 3, mathSubject, 2);
            finalExam.Questions.Add(q1);
            finalExam.Questions.Add(q2);
            finalExam.Questions.Add(q3);

            // ── 6. Ask user to select exam type ───────────
            Console.WriteLine("=======================================");
            Console.WriteLine("       SELECT YOUR EXAM TYPE           ");
            Console.WriteLine("=======================================");
            Console.WriteLine("  1 - Practice Exam");
            Console.WriteLine("  2 - Final Exam");
            Console.WriteLine("=======================================");
            Console.Write("Enter your choice: ");

            Exam selectedExam;
            int choice = 0;

            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
                {
                    Console.Write("Invalid choice. Please enter 1 or 2: ");
                    continue;
                }
                break;
            }

            selectedExam = choice == 1 ? practiceExam : (Exam)finalExam;

            Console.WriteLine();
            Console.WriteLine($"You selected: {(choice == 1 ? "Practice Exam" : "Final Exam")}");
            Console.WriteLine();

            // ── 7. Change Mode to Starting then launch ────
            selectedExam.Mode = ExamMode.Starting;

            Console.WriteLine($"Exam Status : {selectedExam.Mode}");
            Console.WriteLine("Starting in 3 seconds...");
            System.Threading.Thread.Sleep(3000);

            // Start() internally calls:
            //   ShowExam() → displays exam info + questions
            //   loops      → collects student answers
            //   Finish()   → shows results based on exam type
            selectedExam.Start();

            #region old main
            /*
            
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


             */
            #endregion
        }
    }
}

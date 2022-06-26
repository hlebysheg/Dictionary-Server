using WordBook.Helpers.RequestHelpersShema;
using WordBook.Helpers.ResponseHelpersShema;
using WordBook.Models;

namespace WordBook.reposit
{
    public class _testRep
    {
        private readonly ApplicationDbContext db;
        public _testRep(ApplicationDbContext context)
        {
            db = context;
        }

        public TestResponse? getAllByAthor (string name)
        {
            Student student = db.Student.FirstOrDefault(p => p.Name == name);
            var dictionary = db.Dictionary.Where(a  => a.Author.Id == student.Id).ToList();

            if (student == null)
                return null;

            Test test = new Test
            {
                CreationTime = DateTime.UtcNow,
            };
            
            try
            {
                db.Tests.Add(test);
                db.SaveChanges();
                test = db.Tests.FirstOrDefault(p => p.CreationTime == test.CreationTime);
            }
            catch (Exception ex)
            {
                return null;
            }
            TestToStudent testToStudent = new TestToStudent
            {
                Student = student,
                Test = test,
            };
            try
            {
                db.TestToStudents.Add(testToStudent);
                db.SaveChanges();
            }
            catch (Exception ex)
            { }



            var letters = new List<Letter>();
            var letterTest = new List<TestToLetter>();

            foreach (Dictionary dict in dictionary)
            {
                var let = db.Letters.Where(a => a.DictionaryId == dict.Id).ToList();
                letters.AddRange(let);
            }

            foreach (Letter letter in letters)
            {
                Answer ans = new Answer
                {
                    Correct = false,
                    Word = "",
                    Translate = ""
                };
                db.Answers.Add(ans);

                var record = new TestToLetter { Letter = letter, Test = test, Answer = ans };
                db.TestToLetters.Add(record);
            }

            try
            {
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                return null;
            }
            
            return new TestResponse {
                Date=test.CreationTime,
                Id=test.Id,
                LetterResponses = LetterToResponse.createTestResponse(letters),
            };
        }

        public TestResultRespones? FinshTest(TestResultRequest TestResult)
        {
            Test test = db.Tests.Find(TestResult.Id);

            if (test == null)
                return null;

            List<int> IdLetter = db.TestToLetters.Where(a => a.TestId == test.Id).Select(p => p.LetterId).ToList();
            List<int> IdAnswer = db.TestToLetters.Where(a => a.TestId == test.Id).Select(p => p.LetterId).ToList();

            List<Letter> letterActual = db.Letters.Where(a => IdLetter.Contains(a.Id)).ToList();
            List<Answer> answerActual = db.Answers.Where(a => IdAnswer.Contains(a.Id)).ToList();

            List<Answer> answer = LetterToAnswer.CreateAnswer(TestResult, letterActual);

            if (answer.Count != answerActual.Count || answer == null)
                return null;

            int trueAnswer = 0;
            //true score
            for(int i = 0; i < answer.Count; i++)
            {
                if (answer[i].Correct)
                {
                    trueAnswer++;
                }
                answerActual[i].Translate = answer[i].Translate;
                answerActual[i].Word = answer[i].Word;
                answerActual[i].Correct = answer[i].Correct;
            }
            db.SaveChanges();

            var score = (double) trueAnswer / (double) answer.Count;
            if (score >= 0.95)
                test.Score = Score.S;
            else if (score >= 0.85)
                test.Score = Score.A;
            else if (score >= 0.7)
                test.Score = Score.B;
            else if (score >= 0.5)
                test.Score = Score.C;
            else
            {
                test.Score = Score.D;
            }
            test.FinalTime = TestResult.FinalTime;
            db.SaveChanges();

            return new TestResultRespones { Test = test, Answers = answerActual};
        }
    }
}

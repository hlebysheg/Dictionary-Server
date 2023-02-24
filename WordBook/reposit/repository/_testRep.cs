using WordBook.Helpers.RequestHelpersShema;
using WordBook.Helpers.ResponseHelpersShema;
using WordBook.Models;
using WordBook.reposit.Interface;

namespace WordBook.reposit
{
    public class _testRep: ITestRep
    {
        private readonly ApplicationDbContext db;
        public _testRep(ApplicationDbContext context)
        {
            db = context;
        }

        public TestResponse? CreateTestByName (string name)
        {
            StudentLogin student = db.StudentLogin.FirstOrDefault(p => p.Name == name);
            var dictionary = db.Dictionary.Where(a  => a.Author.Id == student.Id).ToList();

            if (student == null)
                return null;

            List<int> dictId = new List<int>();
            foreach (var item in dictionary) dictId.Add(item.Id);

            return CreateTestByDict(dictId, student);
        }

        public TestResponse? CreateTestByDict(List<int> dictId, StudentLogin? student = null)
        {
            List<Letter> letters = db.Letters.Where(a => dictId.Contains(a.DictionaryId) ).ToList();

            Test test = new Test
            {
                CreationTime = DateTime.UtcNow,
            };

            db.Tests.Add(test);

            db.TestToStudents.Add(new TestToStudent
            {
                StudentLogin = student,
                Test = test,
            });

            foreach (Letter letter in letters)
            {
                db.Answers.Add(new Answer
                {
                    Correct = false,
                    Word = "",
                    Translate = "",
                    LetterId = letter.Id,
                    Test = test,
                });
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return null;
            }

            return new TestResponse
            {
                Date = test.CreationTime,
                Id = test.Id,
                LetterResponses = LetterToResponse.createTestResponse(letters),
            };
        }

        public Test? FinshTest(TestResultRequest TestResult)
        {
            Test test = db.Tests.Find(TestResult.Id);

            if (test == null)
                return null;
           
            var answerActual = db.Answers.Where(a => a.TestId == test.Id).ToList();
            int trueAnswer = 0;

            foreach (var item in answerActual.Select((value, i) => new {i, value}))
            {
                LetterResponse letterWithAnswer = TestResult.LetterAnswer[item.i];
                Letter? letter = db.Letters.Find(item.value.LetterId);

                if (letter == null) 
                    return null;

                bool isWordCorrect = letterWithAnswer.Word == letter.Word;
                bool isTranslateCorrect = letterWithAnswer.Translate == letter.Translate;
                item.value.Correct = isWordCorrect && isTranslateCorrect;

                if(item.value.Correct) 
                    trueAnswer++;
                
                item.value.Word = letterWithAnswer.Word;
                item.value.Translate = letterWithAnswer.Translate;
            }

            var percent = (double) trueAnswer / (double)answerActual.Count;
            test.Score = SetScore(percent);
            test.FinalTime = TestResult.FinalTime;

            db.SaveChanges();
            foreach (var item in test.Answer)
            {
                item.Test = null;
                item.Letter = null;
            }
            return test;
        }

        private Score SetScore(double percent)
        {
            //delete magick numbers
            if (percent >= 0.95)
                return Score.S;
            else if (percent >= 0.85)
                return Score.A;
            else if (percent >= 0.7)
                return Score.B;
            else if (percent >= 0.5)
                return Score.C;
            else
            {
                return Score.D;
            }
        }
    }
}

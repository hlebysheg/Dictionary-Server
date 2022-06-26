using WordBook.Models;

namespace WordBook.Helpers.ResponseHelpersShema
{
    public class TestResponse
    {
        public int Id { get; set; }
        public List<LetterResponse> LetterResponses { get; set; }
        public DateTime Date { get; set; }  
    }

    public class TestResultRespones
    {
        public Test Test { get; set; }
        public List<Answer> Answers { get; set; }
    }
}

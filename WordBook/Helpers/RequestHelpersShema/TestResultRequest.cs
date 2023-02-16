using WordBook.Helpers.ResponseHelpersShema;
using WordBook.Models;

namespace WordBook.Helpers.RequestHelpersShema
{
    public class TestResultRequest
    {
        public int Id { get; set; }//testId
        public List<LetterResponse> LetterAnswer { get; set; }
        public DateTime FinalTime { get; set; }
    }

    public static class LetterToAnswer
    {
        
    }
}

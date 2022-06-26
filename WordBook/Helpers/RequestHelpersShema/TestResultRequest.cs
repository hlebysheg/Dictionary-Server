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
        public static List<Answer>? CreateAnswer(TestResultRequest TestResultRequest, List<Letter> lettersActual)
        {
            var letters = TestResultRequest.LetterAnswer;
            List<Answer> answers = new List<Answer>();

            if(lettersActual.Count != letters.Count)
            {
                return null;
            }

            for (int i = 0; i < lettersActual.Count; i++)
            {
                bool isTranslateCorrect = letters[i].Translate == lettersActual[i].Translate;
                bool isWordCorrect = letters[i].Word == lettersActual[i].Word;

                Answer  answer = new Answer { 
                    Translate = letters[i].Translate,
                    Word = letters[i].Word,
                    Correct = isTranslateCorrect && isWordCorrect
                };
                answers.Add(answer);
            }

            return answers;
        }
    }
}

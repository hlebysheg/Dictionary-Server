using WordBook.Models;

namespace WordBook.Helpers.ResponseHelpersShema
{
    public class LetterResponse
    {
        public string? Word { get; set; }
        public string? Translate { get; set; }
        public string? Anotation { get; set; }
        public int Id { get; set; }
        public int DictId { get; set; }
    }

    public static class LetterToResponse
    {
        public static LetterResponse createResponse(Letter word)
        {
            return new LetterResponse
            {
                Word = word.Word,
                Translate = word.Translate,
                Anotation = word.Anotation,
                Id = word.Id,
                DictId = word.DictionaryId
            };
        }

        public static List<LetterResponse> createResponse(List<Letter> words)
        {
            List<LetterResponse> responses = new List<LetterResponse>();
            foreach (Letter letter in words) { responses.Add(createResponse(letter)); }

            return responses;
        }

        public static List<LetterResponse> createTestResponse(List<Letter> words)
        {
            Random random = new Random();

            List<LetterResponse> responses = new List<LetterResponse>();
            foreach (Letter letter in words) 
            {

                bool typeLetter = random.Next(100) > 80;

                if (typeLetter)
                {
                    letter.Word = "";
                    responses.Add(createResponse(letter));
                }
                else
                {
                    letter.Translate = "";
                    responses.Add(createResponse(letter));
                }
                
            }

            return responses;
        }
    }
}

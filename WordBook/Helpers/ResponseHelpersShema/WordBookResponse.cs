using WordBook.Helpers.ResponseHelpersShema;
using WordBook.Models;

namespace WordBook.Helpers.ResponseHelpers
{
    public class WordBookResponse
    {
        public string Title { get; set; }
        public string Language { get; set; }
        public List<LetterResponse> Letters { get; set; }
        public string Author { get; set; }
        public int Id { get; set; }

    }

}

using WordBook.Helpers.RequestHelpersShema;
using WordBook.Models;

namespace WordBook.reposit.letterRepository
{
    public interface ILetterRepository
    {
        Letter create(LetterRequest letter);
        Letter update(LetterRequest letter, string? authorName);
        List<Letter> get(int id);
        bool delete(int id, string? authorName);
    }
}

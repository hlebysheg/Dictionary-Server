using WordBook.Helpers;
using WordBook.Models;

namespace WordBook.reposit.Interface
{
    public interface IWoordBook
    {
        Dictionary create(WordBookRequest book, string? authorName);
        bool delete(int id, string? authorName);
        Dictionary update(WordBookRequest book, string? authorName);
        List<Dictionary> getByName(string name);

    }
}

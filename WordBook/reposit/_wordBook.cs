
using WordBook.Helpers;
using WordBook.Models;
namespace WordBook.reposit
{
    public class _wordBook
    {
        private readonly ApplicationDbContext db;
        public _wordBook(ApplicationDbContext context)
        {
            db = context;
        }

        public Dictionary? addWordBook(WordBookRequest book, string? authorName)
        {
            Student student = db.Student.FirstOrDefault(p=> p.Name == authorName);
            if (student != null)
            {
                Dictionary wordBook = new Dictionary
                {
                    Title = book.Title,
                    Author = student,
                    language = book.language,
                };
                db.Dictionary.Add(wordBook);
                db.SaveChanges();

                return wordBook;
            }

            return null;
        }

        public bool deleteBook(int id, string? authorName)
        {
            Student student = db.Student.FirstOrDefault(p => p.Name == authorName);
            Dictionary wordBook = db.Dictionary.Find(id);
            List<Letter> letters = db.Letters.Where(p => p.DictionaryId == wordBook.Id).ToList();

            bool isAuthorCorrect = false;
            try
            {
                isAuthorCorrect = student.Name == wordBook.Author.Name;
            }
            catch (Exception ex) 
            {
                return false;
            }
            bool isStudent = student != null;
            bool isWordBook = wordBook != null; 

            if (isAuthorCorrect && isStudent && isWordBook)
            {
                db.Letters.RemoveRange(letters);

                db.Dictionary.Remove(wordBook);
                
                db.SaveChanges();

                return true;
            }

            return false;
        }

        //putBook
        public Dictionary? putBook(WordBookRequest book, string? authorName)
        {
            Student student = db.Student.FirstOrDefault(p => p.Name == authorName);
            Dictionary wordBook = db.Dictionary.Find(book.Id);

            bool isAuthorCorrect = false;
            try
            {
                isAuthorCorrect = student.Name == wordBook.Author.Name;
            }
            catch (Exception ex)
            {
                return null;
            }
            bool isStudent = student != null;
            bool isWordBook = wordBook != null;

            if (isAuthorCorrect && isStudent && isWordBook)
            {
                wordBook.Title = book.Title;
                wordBook.language = book.language;
                db.SaveChanges();

                return wordBook;
            }

            return null;
        }

        public List<Dictionary> getDict(string authorName)
        {
            Student student = db.Student.FirstOrDefault(p => p.Name == authorName);
            List<Dictionary> result = db.Dictionary.Where(p => p.Author == student).ToList();

            return result;
        }
    }
}

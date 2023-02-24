
using Microsoft.EntityFrameworkCore;
using WordBook.Helpers;
using WordBook.Models;
using WordBook.reposit.Interface;

namespace WordBook.reposit
{
    public class _wordBook: IWoordBook
    {
        private readonly ApplicationDbContext db;
        public _wordBook(ApplicationDbContext context)
        {
            db = context;
        }

        public Dictionary? create(WordBookRequest book, string? authorName)
        {
            StudentLogin login = db.StudentLogin.FirstOrDefault(p=> p.Name == authorName);

            if (login != null)
            {
                StudentInfo student = db.StudentInfo.FirstOrDefault(p => p.StudentLoginId == login.Id);

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

        public bool delete(int id, string? authorName)
        {
            StudentLogin? student = db.StudentLogin.FirstOrDefault(p => p.Name == authorName);
            Dictionary? wordBook = db.Dictionary.Include(el => el.Author).FirstOrDefault(el => el.Id == id);
            List<Letter>? letters = db.Letters.Where(p => p.DictionaryId == wordBook.Id).ToList();
            if (student == null || wordBook == null)
                return false;
            StudentInfo std = db.StudentInfo.FirstOrDefault(el => el.StudentLoginId == student.Id);
            bool isAuthorCorrect = std.Id == wordBook.Author.Id;

            if (isAuthorCorrect)
            {
                db.Letters.RemoveRange(letters);

                db.Dictionary.Remove(wordBook);
                
                db.SaveChanges();

                return true;
            }

            return false;
        }

        //putBook
        public Dictionary? update(WordBookRequest book, string? authorName)
        {
            StudentLogin? student = db.StudentLogin.FirstOrDefault(p => p.Name == authorName);
            Dictionary? wordBook = db.Dictionary.Include(el => el.Author).FirstOrDefault(el => el.Id == book.Id);
            List<Letter>? letters = db.Letters.Where(p => p.DictionaryId == wordBook.Id).ToList();

            if (student == null || wordBook == null)
                return null;

            StudentInfo std = db.StudentInfo.FirstOrDefault(el => el.StudentLoginId == student.Id);

            bool isAuthorCorrect = std.Id == wordBook.Author.Id;
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

        public List<Dictionary> getByName(string authorName)
        {
            StudentInfo student = db.StudentInfo.FirstOrDefault(p => p.DisplayName == authorName);
            List<Dictionary> result = db.Dictionary.Where(p => p.Author == student).ToList();

            return result;
        }
    }
}

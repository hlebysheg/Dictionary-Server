using WordBook.Helpers.RequestHelpersShema;
using WordBook.Models;
using WordBook.reposit.letterRepository;

namespace WordBook.reposit
{
    public class _letterRep: ILetterRepository
    {
        private readonly ApplicationDbContext db;
        //ApplicationDbContext context
        public _letterRep(ApplicationDbContext context)
        {
            db = context;
        }

        public Letter? create(LetterRequest letter)
        {
            Dictionary book = db.Dictionary.Find(letter.DictId);
            
            if (book != null)
            {
                Letter word = new Letter
                {
                    Translate = letter.Translate,
                    Word = letter.Word,
                    DictionaryId = book.Id,
                    Anotation = letter.Anotation,
                };
                db.Letters.Add(word);
                db.SaveChanges();

                return word;
            }

            return null;
        }

        public bool delete(int id, string? authorName)
        {
            Student student = db.Student.FirstOrDefault(p => p.Name == authorName);
            Letter letter = db.Letters.Find(id);
            Dictionary wordBook = db.Dictionary.Find(letter.DictionaryId);
            Student author = db.Student.Find(wordBook.Author.Id);

            bool isAuthorCorrect = false;
            try
            {
                isAuthorCorrect = student?.Name == author?.Name;
            }
            catch (Exception ex)
            {
                return false;
            }
            bool isStudent = student != null;
            bool isWordBook = wordBook != null;
            bool isLetter = letter != null;

            if (isAuthorCorrect && isStudent && isWordBook && isLetter)
            {
                db.Letters.Remove(letter);
                db.SaveChanges();

                return true;
            }

            return false;
        }

        //putBook
        public Letter? update(LetterRequest letter, string? authorName)
        {
            Student? student = db.Student.FirstOrDefault(p => p.Name == authorName);
            Dictionary? wordBook = db.Dictionary.Find(letter.DictId);
            Letter? word = db.Letters.Find(letter.Id);
            Student? author = db.Student.Find(wordBook?.Author.Id);


            bool isAuthorCorrect = false;
            try
            {
                isAuthorCorrect = student?.Name == author?.Name;
            }
            catch (Exception ex)
            {
                return null;
            }
            bool isStudent = student != null;
            bool isWordBook = wordBook != null;
            bool isLetter = letter != null;

            if (isAuthorCorrect && isStudent && isWordBook && isLetter)
            {
                word.Anotation = letter.Anotation;
                word.Word = letter.Word;
                word.Translate = letter.Translate;
                db.SaveChanges();

                return word;
            }

            return null;
        }

        public List<Letter>? get(int id)
        {
            Dictionary? book = db.Dictionary.FirstOrDefault(p => p.Id == id);
            
            if(book == null) 
                return null;

            try
            {
                var result = db.Letters.Where(p => p.DictionaryId == book.Id).ToList();
                return result;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}

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
            Letter letter = db.Letters.Find(id);

            if (letter == null)
                return false;

            db.Letters.Remove(letter);
            db.SaveChanges();

            return true;
        }

        //putBook
        public Letter? update(LetterRequest letter, string? authorName)
        {
            Letter word = db.Letters.Find(letter.Id);

            if (word != null)
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

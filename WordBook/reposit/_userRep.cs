using WordBook.Helpers;
using WordBook.Models;

namespace WordBook.reposit
{
    public class _userRep
    {
        private readonly ApplicationDbContext db;
        public _userRep(ApplicationDbContext context)
        {
            db = context;
        }
        public Student? Auth(string? name, string? pass)
        {
            Student? strudent = db.Student.FirstOrDefault(p => p.Name == name);
            if (strudent != null)
            {
                if(HashPassword.Verif(pass, strudent.Password))
                {
                    return strudent;
                }
            }
            return null;
        }

        public string Reg(string name, string pass, string email)
        {
            Student? IsStudentName = db.Student.FirstOrDefault(p => p.Name == name);
            Student? IsStudentEmail = db.Student.FirstOrDefault(p => p.Email == email);

            if (IsStudentName == null && IsStudentEmail == null)
            {
                Student student = new Student
                {
                    Name = name,
                    Email = email,
                    Password = HashPassword.HashPass(pass)
                };
                db.Student.Add(student);
                db.SaveChanges();

                return "created" ;
            }

            if(IsStudentEmail != null)
            {
                return "try another mail";   
            }

            return "try another name";
        }

        public void setUsed(RefreshToken token)
        {
            token.Used = true;
            db.RefreshTokens.Update(token);
            db.SaveChanges();
        }

        public Student? getUserByToken(RefreshToken token) 
        {
            return db.Student.FirstOrDefault(p => p.Id == token.StudentId);
        }

        public RefreshToken? FindToken (string token)
        {
            return db.RefreshTokens.SingleOrDefault(p => p.Token == token);
        }

        public void saveToken (RefreshToken refTokenToResponse)
        {
            db.RefreshTokens.Add(refTokenToResponse);
            db.SaveChanges();
        }

        public bool deleteToken (string token)
        {
            try
            {
                var tk = db.RefreshTokens.SingleOrDefault(p => p.Token == token);
                db.RefreshTokens.Remove(tk);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

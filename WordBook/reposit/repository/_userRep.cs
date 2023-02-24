using WordBook.Helpers;
using WordBook.Models;
using WordBook.reposit.Interface;

namespace WordBook.reposit
{
    public class _userRep: IAuthRep
    {
        private readonly ApplicationDbContext db;
        public _userRep(ApplicationDbContext context)
        {
            db = context;
        }

        private void setUsed(RefreshToken token)
        {
            token.Used = true;
            db.RefreshTokens.Update(token);
            db.SaveChanges();
        }

        public void create(RefreshToken refTokenToResponse)
        {
            db.RefreshTokens.Add(refTokenToResponse);
            db.SaveChanges();
        }

        public StudentLogin? Auth(string? email, string? pass)
        {
            StudentLogin? strudent = db.StudentLogin.FirstOrDefault(p => p.Email == email);
            if (strudent != null)
            {
                if(HashPassword.Verif(pass, strudent.Password))
                {
                    return strudent;
                }
            }
            return null;
        }

        public bool Reg(string name, string pass, string email)
        {
            StudentLogin? IsStudentName = db.StudentLogin.FirstOrDefault(p => p.Name == name);
            StudentLogin? IsStudentEmail = db.StudentLogin.FirstOrDefault(p => p.Email == email);

            if (IsStudentName == null && IsStudentEmail == null)
            {
                StudentLogin student = new StudentLogin
                {
                    Name = name,
                    Email = email,
                    Password = HashPassword.HashPass(pass)
                };
                StudentInfo info = new StudentInfo { DisplayName = name, StudentLogin = student};

                db.StudentLogin.Add(student);
                db.StudentInfo.Add(info);

                db.SaveChanges();

                return true ;
            }

            return false;
        }

        public StudentLogin? getUserByToken(RefreshToken token) 
        {
            return db.StudentLogin.FirstOrDefault(p => p.Id == token.StudentId);
        }

        public RefreshToken? TokenFind(string token)
        {
            RefreshToken? tkn = db.RefreshTokens.SingleOrDefault(p => p.Token == token);

            if (tkn == null 
                || tkn.ExpiryData < DateTime.UtcNow 
                || tkn.Used)
            {
                return null;
            }

            setUsed(tkn);

            return tkn;
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

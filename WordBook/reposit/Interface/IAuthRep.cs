using WordBook.Models;

namespace WordBook.reposit.Interface
{
    public interface IAuthRep
    {
        void create(RefreshToken refTokenToResponse);
        StudentLogin? Auth(string? email, string? pass);
        bool Reg(string name, string pass, string email);
        StudentLogin? getUserByToken(RefreshToken token);
        RefreshToken? TokenFind(string token);
        bool deleteToken(string token);
    }
}

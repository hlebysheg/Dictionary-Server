using WordBook.Models;

namespace WordBook.reposit.Interface
{
    public interface IAuthRep
    {
        void create(RefreshToken refTokenToResponse);
        Student? Auth(string? name, string? pass);
        bool Reg(string name, string pass, string email);
        Student? getUserByToken(RefreshToken token);
        RefreshToken? TokenFind(string token);
        bool deleteToken(string token);
    }
}

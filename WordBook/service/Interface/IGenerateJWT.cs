using WordBook.Models;

namespace WordBook.service.Interface
{
    public interface IGenerateJWT
    {
        string Generate(StudentLogin student);
        string GenerateRandomStr(int len);
    }
}

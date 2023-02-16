using WordBook.Models;

namespace WordBook.service.Interface
{
    public interface IGenerateJWT
    {
        string Generate(Student student);
        string GenerateRandomStr(int len);
    }
}

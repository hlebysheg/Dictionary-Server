using WordBook.Helpers.RequestHelpersShema;
using WordBook.Helpers.ResponseHelpersShema;
using WordBook.Models;

namespace WordBook.reposit.Interface
{
    public interface ITestRep
    {
        TestResponse? CreateTestByName(string name);
        TestResponse? CreateTestByDict(List<int> dictId, StudentLogin? student);
        Test? FinshTest(TestResultRequest TestResult);
    }
}

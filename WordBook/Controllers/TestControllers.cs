using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WordBook.Helpers;
using WordBook.Helpers.RequestHelpersShema;
using WordBook.Helpers.ResponseHelpers;
using WordBook.Helpers.ResponseHelpersShema;
using WordBook.Models;
using WordBook.reposit;
using WordBook.reposit.Interface;

namespace WordBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestControllers : ControllerBase
    {
        private readonly ILogger<UserLogin> _logger;
        private readonly ITestRep db;
        private IConfiguration _conf;
        public TestControllers(ITestRep context, IConfiguration config, ILogger<UserLogin>? log)
        {

            db = context;
            _conf = config;
            _logger = log;
        }

        [HttpGet]
        [Route("get/bigtest")]
        [Authorize]
        public IActionResult getTest() 
        {
            string name = User.Identity.Name;
            TestResponse? letter = db.CreateTestByName(name);

            if (letter == null)
            {
                return Ok();
            }
            return Ok(letter);
        }

        [HttpPost]
        [Route("finish/bigtest")]
        [Authorize]
        public IActionResult FinishTest([FromBody] TestResultRequest test)
        {
            string name = User.Identity.Name;
            Test? testResult = db.FinshTest(test);

            if (testResult == null)
            {
                return BadRequest();
            }
            return Ok(testResult);
        }
    }
}

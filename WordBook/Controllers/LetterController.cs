using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WordBook.Helpers.RequestHelpersShema;
using WordBook.Helpers.ResponseHelpersShema;
using WordBook.Models;
using WordBook.reposit;
using WordBook.reposit.letterRepository;

namespace WordBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LetterController : ControllerBase
    {
        private readonly ILogger<UserLogin> _logger;
        private readonly ILetterRepository db;
        private IConfiguration _conf;
        public LetterController(ILetterRepository context, IConfiguration config, ILogger<UserLogin>? log)
        {

            db = context;
            _conf = config;
            _logger = log;
        }

        [HttpPost]
        [Route("create/letter")]
        [Authorize]
        public IActionResult CreateLetter([FromBody] LetterRequest letter)
        {//add User.identity.name == book.author
            
            Letter? word = db.create(letter);

            if (word != null)
            {
                LetterResponse wdResonse = LetterToResponse.createResponse(word);

                return Ok(wdResonse);
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("delete/letter/{id}")]
        [Authorize]
        public IActionResult DeleteWordBook(int id)
        {//add User.identity.name == book.author

            string name = User.Identity.Name;
            bool isDeleteWord = db.delete(id, name);

            if (isDeleteWord)
            {
                return Ok();
            }

            return BadRequest("cant delete");
        }

        [HttpPut]
        [Route("put/letter")]
        [Authorize]
        public IActionResult PutWordBook([FromBody] LetterRequest wrReq)
        {//add User.identity.name == book.author

            string name = User.Identity.Name;


            Letter? putBook = db.update(wrReq, name);

            if (putBook != null)
            {
                LetterResponse wdResonse = LetterToResponse.createResponse(putBook);

                return Ok(wdResonse);
            }

            return BadRequest("cant put");
        }

        [HttpGet]
        [Route("get/letter/{id}")]
        [Authorize]
        public IActionResult GetLetter(int id)
        {

            List<Letter>? letters = db.get(id);

            if (letters != null)
            {
                var responses = LetterToResponse.createResponse(letters);

                return Ok(responses);
            }

            return BadRequest("cant get");
        }     
    }
}

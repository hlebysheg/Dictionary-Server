using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WordBook.Helpers;
using WordBook.Helpers.ResponseHelpers;
using WordBook.Models;
using WordBook.reposit;
using WordBook.reposit.Interface;

namespace WordBook.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class WordBook : ControllerBase
    {
        private readonly ILogger<UserLogin> _logger;
        private readonly IWoordBook db;
        private IConfiguration _conf;
        public WordBook(IWoordBook context, IConfiguration config, ILogger<UserLogin>? log)
        {

            db = context;
            _conf = config;
            _logger = log;
        }

        [HttpPost]
        [Route("create/wordbook")]
        [Authorize]
        public IActionResult CreateWordBook([FromBody] WordBookRequest book)
        {//add User.identity.name == book.author
            string name = User.Identity.Name;
            Dictionary? wordBook = db.create(book, name);

            if (wordBook != null)
            {
                WordBookResponse wdResonse = createResponse(wordBook);

                return Ok(wdResonse);
            }

            return BadRequest(); 
        }

        [HttpDelete]
        [Route("delete/wordbook/{id}")]
        [Authorize]
        public IActionResult DeleteWordBook(int id)
        {//add User.identity.name == book.author

            string name = User.Identity.Name;
            bool isDeleteWordBook = db.delete(id, name);

            if (isDeleteWordBook)
            {
                return Ok();
            }

            return BadRequest("cant delete");
        }

        [HttpPut]
        [Route("put/wordbook")]
        [Authorize]
        public IActionResult PutWordBook([FromBody] WordBookRequest wdReq)
        {//add User.identity.name == book.author

            string name = User.Identity.Name;

            if (name != wdReq.Author)
            {
                return BadRequest("cant put");
            }

            Dictionary PutBook = db.update(wdReq, name);

            if (PutBook != null)
            {
                WordBookResponse wdResonse = createResponse(PutBook);

                return Ok(wdResonse);
            }

            return BadRequest("cant put");
        }

        [HttpGet]
        [Route("get/wordbook/{author}")]
        [Authorize]
        public IActionResult GetDicts(string author)
        {//add User.identity.name == book.author

            string name = User.Identity.Name;

            if (name != author)
            {
                return BadRequest("cant put");
            }

            List<Dictionary> dicts = db.getByName(name);

            if (dicts != null)
            {
                List<WordBookResponse> responses = new List<WordBookResponse>();
                foreach (Dictionary dict in dicts) { responses.Add(createResponse(dict)); }

                return Ok(responses);
            }

            return BadRequest("cant get");
        }

        //helpers
        private WordBookResponse createResponse (Dictionary dir)
        {
            return new WordBookResponse
            {
                Title = dir.Title,
                Language = dir.language,
                Author = dir.Author.Name,
                Id = dir.Id,
            };
        }

    }
}

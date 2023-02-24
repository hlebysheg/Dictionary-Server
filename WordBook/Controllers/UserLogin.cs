using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WordBook.Helpers;
using WordBook.Helpers.RequestHelpersShema;
using WordBook.Models;
using WordBook.reposit;
using WordBook.reposit.Interface;
using WordBook.service.Interface;

namespace WordBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLogin : ControllerBase
    {
        private readonly ILogger<UserLogin> _logger;
        private readonly IAuthRep db;
        private IConfiguration _conf;
        IGenerateJWT _generator;
        public UserLogin(IAuthRep context, IConfiguration config, ILogger<UserLogin>? log, IGenerateJWT generator)
        {

            db = context;
            _conf = config;
            _logger = log;
            _generator = generator;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("log")]
        public IActionResult Login([FromBody] StudentRequest logData)
        {
            var student = db.Auth(logData.Email, logData.Password);
            if (student != null)
            {
                var token = _generator.Generate(student);
                var TokenContext = _generator.GenerateRandomStr(25);
                var refTokenToResponse = new RefreshToken
                {
                    Used = false,
                    CreationTime = DateTime.UtcNow,
                    ExpiryData = DateTime.UtcNow.AddMonths(6),
                    StudentId = student.Id,
                    Student = student,
                    Token = TokenContext + Guid.NewGuid()
                };
                db.create(refTokenToResponse);

                TokenResponse resp = new TokenResponse
                {
                    RefreshToken = refTokenToResponse.Token,
                    AccesToken = _generator.Generate(student)
                };

                return Ok(new { resp, student.Name });
            }
            return NotFound("User not found");
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenRequest token)
        {
            string refreshToken = token.Token;

            var storedToken = db.TokenFind(refreshToken);
            if(storedToken == null)
            {
                return BadRequest("auth fail");
            }

            var user = db.getUserByToken(storedToken);

            if (user == null)
            {
                return BadRequest("user not found");
            }
            //generate token
            var TokenContext = _generator.GenerateRandomStr(25);
            var refTokenToResponse = new RefreshToken
            {
                Used = false,
                CreationTime = DateTime.UtcNow,
                ExpiryData = DateTime.UtcNow.AddMonths(6),
                StudentId = user.Id,
                Student = user,
                Token = TokenContext + Guid.NewGuid()
            };
            db.create(refTokenToResponse);

            TokenResponse resp = new TokenResponse
            {
                RefreshToken = refTokenToResponse.Token,
                AccesToken = _generator.Generate(user)
            };

            return Ok(resp); 
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("logout")]
        public IActionResult Logout([FromBody] TokenRequest token)
        {
            string refreshToken = token.Token;

            var storedToken = db.TokenFind(refreshToken);
            if (storedToken == null)
            {
                return NotFound("token not found");
            }

            if (db.deleteToken(refreshToken))
            {
                return Ok("Logout");
            }

            return BadRequest("token not found");
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("reg")]
        public IActionResult Register([FromBody] StudentRequest student)
        {
            var isCreated = db.Reg(student.Name ,student.Password, student.Email);
            //var isReg = true;
            if(isCreated)
            {
                return Ok("create");
            }
            return BadRequest("try another login or email");
        }
        
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PersonApiDAL.Context;
using PersonApiDAL.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Text;

namespace PersonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration _config;
        private readonly JWTContext _context;

        public LoginController(IConfiguration config, JWTContext context)
        {
            _config = config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserInfo login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string GenerateJSONWebToken(UserInfo userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserInfo AuthenticateUser(UserInfo login)
        {
            UserInfo user = null;

            var found = _context.UserInfo.FirstOrDefault(x => x.Username == login.Username && x.Password == login.Password);

            return found;


            //Validate the User Credentials
            //Demo Purpose, I have Passed HardCoded User Information
            //if (login.Username == "Jignesh")
            //{
            //    user = new UserInfo { Username = "Jignesh Trivedi", Password = "test.btest@gmail.com" };
            //}
            //return user;
        }
    }
}

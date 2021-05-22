using Application.Core.DbContexts;
using Application.Core.DbEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Utils;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class AuthController : Controller
    {
        private readonly MainDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public AuthController(MainDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterCredentialsModel creds)
        {
            var existingUser = await _dbContext.AppUsers.FirstOrDefaultAsync(x => x.Email == creds.Email);
            if(existingUser != null)
            {
                return Json(new BaseResponseModel()
                {
                    Success = false,
                    Error = "Epasts aizņemts!"
                });
            }

            var newUser = new AppUser()
            {
                Email = creds.Email,
                PasswordHash = AuthUtils.GetHashString(creds.Password)
            };
            _dbContext.AppUsers.Add(newUser);

            await _dbContext.SaveChangesAsync();

            return Json(new BaseResponseModel());
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] RegisterCredentialsModel creds)
        {
            var user = await _dbContext.AppUsers.FirstOrDefaultAsync(x => x.Email == creds.Email);
            if (user == null)
            {
                return Json(new BaseResponseModel()
                {
                    Success = false,
                    Error = "Lietotājs nav atrasts!"
                });
            }

            if(user.PasswordHash != AuthUtils.GetHashString(creds.Password)){
                return Json(new BaseResponseModel()
                {
                    Success = false,
                    Error = "Nepareiza parole!"
                });
            };

            return Json(new LoginResponseModel()
            {
                Token = AuthUtils.GenerateJSONWebToken(_configuration["Jwt:Key"]),
                Email = user.Email
            });
        }

        


    }
}

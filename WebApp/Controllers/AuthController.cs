using Application.Core.DbContexts;
using Application.Core.DbEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public AuthController(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterCredentialsModel creds)
        {
            var existingUser = await _dbContext.AppUsers.FirstOrDefaultAsync(x => x.Email == creds.Email);
            if(existingUser != null)
            {
                return Ok(new BaseResponseModel()
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

            return Ok(new BaseResponseModel());
        }


    }
}

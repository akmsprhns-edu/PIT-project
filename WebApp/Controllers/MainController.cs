using Application.Core.DbContexts;
using Application.Core.DbEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class MainController : Controller
    {
        private readonly MainDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public MainController(MainDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRootCategories()
        {
            var categories = await _dbContext.AdvertCategories.Include(x => x.SubCategories).Where(x => x.ParentCategoryId == null).ToListAsync();
            return Json(categories);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAdvert([FromBody] NewAdvertModel data)
        {
            if (!ModelState.IsValid)
                return Json(new BaseResponseModel()
                {
                    Success = false,
                    Error = string.Join('\n', ModelState.Values.SelectMany(x => x.Errors))
                });

            var newAdvert = new AdvertItem()
            {
                Title = data.Title,
                Descirption = data.Description,
                DateCreated = DateTime.Now,
                Price = data.Price
            };

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                newAdvert.UserId = long.Parse(identity.FindFirst("UserId").Value);
            }

            _dbContext.AdvertItems.Add(newAdvert);
            await _dbContext.SaveChangesAsync();

            return Json(new BaseResponseModel());
        }
    }
}

using Application.Core.DbContexts;
using Application.Core.DbEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
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
        public async Task<IActionResult> CreateAdvert([FromForm] NewAdvertModel data)
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
                Price = data.Price,
                CategoryId = data.CategoryId
            };

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                newAdvert.UserId = long.Parse(identity.FindFirst("UserId").Value);
            }

            _dbContext.AdvertItems.Add(newAdvert);
            if (data.Images != null)
            {
                if(data.Images.Count > 10)
                {
                    return Json(new BaseResponseModel()
                    {
                        Success = false,
                        Error = "Atļauts pievienot ne vairāk ka 10 attēlus"
                    });
                }

                string imageStorageDir = Path.Combine(_configuration["FileStorageDirectory"], "AdvertImages");
                if (!Directory.Exists(imageStorageDir)) 
                    Directory.CreateDirectory(imageStorageDir);

                foreach (var image in data.Images)
                {
                    if (image.Length > 0)
                    {
                        string filePath = Path.Combine(imageStorageDir, Guid.NewGuid().ToString() + image.FileName);
                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }
                        _dbContext.AdvertImages.Add(new AdvertImage()
                        {
                            Advert = newAdvert,
                            Path = filePath.Substring(_configuration["FileStorageDirectory"].Length)
                        });
                    }
                }
            }

            await _dbContext.SaveChangesAsync();

            return Json(new BaseResponseModel());
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> GetAdverts([FromBody] GetAdvertsRequestModel request)
        {
            var query = _dbContext.AdvertItems.Include(x => x.Images).Include(x => x.Category).AsQueryable();
            if (request.CategoryId != null)
            {
                query = query.Where(x => x.CategoryId == request.CategoryId.Value || x.Category.ParentCategoryId == request.CategoryId.Value);
            }

            if (request.SortOrder is null || request.SortOrder == SortOrder.DateAsc)
                query = query.OrderBy(x => x.DateCreated);
            else if ( request.SortOrder == SortOrder.DateDesc)
                query = query.OrderByDescending(x => x.DateCreated);
            else if (request.SortOrder == SortOrder.PriceAsc)
                query = query.OrderBy(x => x.Price);
            else if (request.SortOrder == SortOrder.PriceDesc)
                query = query.OrderByDescending(x => x.Price);

            var data = await query.ToListAsync();

            var response = data.Select(x => new AdvertShortInfoModel()
            {
                AdvertId = x.Id,
                Title = x.Title,
                Price = x.Price,
                ImagePath = x.Images.FirstOrDefault()?.Path
            });

            return Json(response);

        }
    }
}

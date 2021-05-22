using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class AppController : ControllerBase
    {
        [HttpGet("[action]")]
        public IActionResult Ping()
        {
            return Ok("Pong");
        }

        [HttpGet("[action]"), Authorize]
        public IActionResult AuthPing()
        {
            return Ok("You are authorized!");
        }
    }
}

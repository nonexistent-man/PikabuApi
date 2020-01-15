using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PikabuApi.Helpers;
using PikabuApi.Models;
using PikabuApi.Parsers;

namespace PikabuApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class OtherController : ControllerBase
    {
        PikabuDbContext _db;

        public OtherController(PikabuDbContext db)
        {
            _db = db;
        }


        [HttpGet("user/{userName}")]
        public async Task<User> GetUser(string userName)
        {
            User result = null;
            UserParser parser = new UserParser();
            var htmlSource = await NetworkHelper.GetHtmlPageSource("https://pikabu.ru/@" + userName);
            if(htmlSource != null)
            {
                result = await parser.ParseAsync(htmlSource);
            }
            return result;
        }
    }
}
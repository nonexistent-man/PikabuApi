using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PikabuApi.Controllers
{
    [Route("api")]
    public class PikabuController : Controller
    {
        PikabuDbContext _db;

        public PikabuController(PikabuDbContext dbContext)
        {
            _db = dbContext;
        }


    }
}

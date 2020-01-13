using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikabuApi.Parsers
{
    interface IParser<T> where T : class
    {
        public Task<T> ParseAsync(string htmlSource);
    }
}

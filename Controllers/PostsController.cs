using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PikabuApi.Helpers;
using PikabuApi.Models;
using PikabuApi.Parsers;

namespace PikabuApi.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        PikabuDbContext _db;

        public PostsController(PikabuDbContext dbContext)
        {
            _db = dbContext;
            
        }

        // /api/posts/id/1234567
        [HttpGet("id/{postId}")]
        public async Task<Post> GetSinglePost(int postId)
        {
            PostsParser parser = new PostsParser();
            Post result = null;
            var htmlSource = await NetworkHelper.GetHtmlPageSource("https://pikabu.ru/story/_" + postId);

            if(htmlSource != null)
            {
                var resultList = await parser.ParseAsync(htmlSource);
                if(resultList != null && resultList.Count != 0)
                {
                    result = resultList[0];
                }
            }

            return result;
        }

        // /api/posts/hot?p=1&c=2
        // p - page number
        // c - page count
        [HttpGet("hot")]
        public async Task<List<Post>> GetHotPosts()
        {
            PostsParser parser = new PostsParser();
            List<Post> result = new List<Post>();
            int page = 1, count = 1;

            if(Request.Query.ContainsKey("p") && Request.Query["p"].Count == 1)
            {
                int.TryParse(Request.Query["p"][0], out page);
            }

            if (Request.Query.ContainsKey("c") && Request.Query["c"].Count == 1)
            {
                int.TryParse(Request.Query["c"][0], out count);
            }

            count = count > 5 ? 5 : count;
            count = count < 1 ? 1 : count;
            page = page < 1 ? 1 : page;

            for (int i = page; i < page + count; i++)
            {
                string htmlSource = await NetworkHelper.GetHtmlPageSource("https://pikabu.ru/hot?page=" + i);

                if (htmlSource != null)
                {
                    var resultList = await parser.ParseAsync(htmlSource);
                    if(resultList != null)
                    {
                        result.AddRange(resultList);
                    }
                }
            }

            return result;
        }

        // /api/posts/best?p=1&c=2
        // /api/posts/best?d=01-01-2020&p=1&c=2
        // d - date
        // p - page number
        // c - page count
        [HttpGet("best")]
        public async Task<List<Post>> GetBestPosts()
        {
            PostsParser parser = new PostsParser();
            List<Post> result = new List<Post>();
            int page = 1, count = 1;
            DateTime date = new DateTime(1998, 8, 16);

            if (Request.Query.ContainsKey("p") && Request.Query["p"].Count == 1)
            {
                int.TryParse(Request.Query["p"][0], out page);
            }

            if (Request.Query.ContainsKey("c") && Request.Query["c"].Count == 1)
            {
                int.TryParse(Request.Query["c"][0], out count);
            }

            if (Request.Query.ContainsKey("d") && Request.Query["d"].Count == 1)
            {
                int.TryParse(Request.Query["d"][0], out count);

                try
                {
                    date = DateTime.ParseExact(Request.Query["d"][0], "dd-MM-yyyy", CultureInfo.InvariantCulture);
                }
                catch
                {
                    return result;
                }
            }

            count = count > 5 ? 5 : count;
            count = count < 1 ? 1 : count;
            page = page < 1 ? 1 : page;

            string url = "https://pikabu.ru/best?page=";
            if(date == new DateTime(1998, 8, 16))
            {
                url = "https://pikabu.ru/best/" + date.ToString("dd-MM-yyyy") + "?page=";
            }

            for (int i = page; i < page + count; i++)
            {

                string htmlSource = await NetworkHelper.GetHtmlPageSource(url + i);

                if (htmlSource != null)
                {
                    var resultList = await parser.ParseAsync(htmlSource);
                    if (resultList != null)
                    {
                        result.AddRange(resultList);
                    }
                }
            }

            return result;
        }

        // /api/posts/new?p=1&c=2
        // p - page number
        // c - page count
        [HttpGet("new")]
        public async Task<List<Post>> GetNewPosts()
        {
            PostsParser parser = new PostsParser();
            List<Post> result = new List<Post>();
            int page = 1, count = 1;

            if (Request.Query.ContainsKey("p") && Request.Query["p"].Count == 1)
            {
                int.TryParse(Request.Query["p"][0], out page);
            }

            if (Request.Query.ContainsKey("c") && Request.Query["c"].Count == 1)
            {
                int.TryParse(Request.Query["c"][0], out count);
            }

            count = count > 5 ? 5 : count;
            count = count < 1 ? 1 : count;
            page = page < 1 ? 1 : page;

            for (int i = page; i < page + count; i++)
            {
                string htmlSource = await NetworkHelper.GetHtmlPageSource("https://pikabu.ru/new?page=" + i);

                if (htmlSource != null)
                {
                    var resultList = await parser.ParseAsync(htmlSource);
                    if (resultList != null)
                    {
                        result.AddRange(resultList);
                    }
                }
            }

            return result;
        }

        // /api/posts/user/userName?p=1&c=2
        // p - page number
        // c - page count
        [HttpGet("user/{userName}")]
        public async Task<List<Post>> GetUserPosts(string userName)
        {
            PostsParser parser = new PostsParser();
            List<Post> result = new List<Post>();
            int page = 1, count = 1;

            if (Request.Query.ContainsKey("p") && Request.Query["p"].Count == 1)
            {
                int.TryParse(Request.Query["p"][0], out page);
            }

            if (Request.Query.ContainsKey("c") && Request.Query["c"].Count == 1)
            {
                int.TryParse(Request.Query["c"][0], out count);
            }

            count = count > 5 ? 5 : count;
            count = count < 1 ? 1 : count;
            page = page < 1 ? 1 : page;

            for (int i = page; i < page + count; i++)
            {
                string htmlSource = await NetworkHelper.GetHtmlPageSource("https://pikabu.ru/@" + userName + "?page=" + i);

                if (htmlSource != null)
                {
                    var resultList = await parser.ParseAsync(htmlSource);
                    if (resultList != null)
                    {
                        result.AddRange(resultList);
                    }
                }
            }

            return result;
        }

        // /api/posts/community/communityName?p=1&c=2
        // p - page number
        // c - page count
        [HttpGet("community/{communityName}")]
        public async Task<List<Post>> GetCommunityPosts(string communityName)
        {
            PostsParser parser = new PostsParser();
            List<Post> result = new List<Post>();
            int page = 1, count = 1;

            if (Request.Query.ContainsKey("p") && Request.Query["p"].Count == 1)
            {
                int.TryParse(Request.Query["p"][0], out page);
            }

            if (Request.Query.ContainsKey("c") && Request.Query["c"].Count == 1)
            {
                int.TryParse(Request.Query["c"][0], out count);
            }

            count = count > 5 ? 5 : count;
            count = count < 1 ? 1 : count;
            page = page < 1 ? 1 : page;

            for (int i = page; i < page + count; i++)
            {
                string htmlSource = await NetworkHelper.GetHtmlPageSource("https://pikabu.ru/community/" + communityName +"?page=" + i);

                if (htmlSource != null)
                {
                    var resultList = await parser.ParseAsync(htmlSource);
                    if (resultList != null)
                    {
                        result.AddRange(resultList);
                    }
                }
            }

            return result;
        }
    }
}
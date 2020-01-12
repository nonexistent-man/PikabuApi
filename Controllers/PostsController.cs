using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PikabuApi.Models;

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
        public Post GetSinglePost(int postId)
        {
            return null;
        }

        // /api/posts/hot?p=1&c=2
        // p - page number
        // c - page count
        [HttpGet("hot")]
        public List<Post> GetHotPosts()
        {
            return null;
        }

        // /api/posts/best?p=1&c=2
        // /api/posts/best?d=01-01-2020&p=1&c=2
        // d - date
        // p - page number
        // c - page count
        [HttpGet("best")]
        public List<Post> GetBestPosts()
        {
            return null;
        }

        // /api/posts/new?p=1&c=2
        // p - page number
        // c - page count
        [HttpGet("new")]
        public List<Post> GetNewPosts()
        {
            return null;
        }

        // /api/posts/user/userName?p=1&c=2
        // p - page number
        // c - page count
        [HttpGet("user/{userName}")]
        public List<Post> GetUserPosts(string userName)
        {
            return null;
        }

        // /api/posts/community/communityName?p=1&c=2
        // p - page number
        // c - page count
        [HttpGet("community/{communityName}")]
        public List<Post> GetCommunityPosts(string userName)
        {
            return null;
        }
    }
}

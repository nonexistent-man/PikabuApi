using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikabuApi.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Rating { get; set; }

        public int SubscribersCount { get; set; }

        public int CommentsCount { get; set; }

        public int PostsCount { get; set; }

        public int HotPostsCount { get; set; }

        public int PositiveCount { get; set; }

        public int NegativeCount { get; set; }

        public string Avatar { get; set; }

        public byte Gender { get; set; }

        public DateTime SinceRegisterDate { get; set; }

        public List<Community> Communities { get; set; }
    }
}

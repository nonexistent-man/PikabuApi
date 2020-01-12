using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikabuApi.Models
{
    public class Community
    {
        public int Id { get; set; }

        public string Avatar { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public int PostsCount { get; set; }

        public int SubscribersCount { get; set; }
    }
}

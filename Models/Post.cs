using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PikabuApi.Models
{
    public class Post
    {
        public int Id { get; set; }

        public int PikabuId { get; set; }

        public string Header { get; set; }
        
        public string Author { get; set; }

        public string Tags { get; set; }

        public DateTime CreationDate { get; set; }

        public bool HasRating { get; set; }

        public int? Rating { get; set; }

        public List<PostContent> Content { get; set; }

        public List<Comment> Comments { get; set; }
    }
}

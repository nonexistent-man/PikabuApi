using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikabuApi.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int PikabuId { get; set; }

        public string AuthorName { get; set; }

        public string Content { get; set; }

        public bool HasRating { get; set; }

        public int? Rating { get; set; }

        public DateTime CreationDate { get; set; }

        public List<CommentAttachment> Attachments { get; set; }
    }
}

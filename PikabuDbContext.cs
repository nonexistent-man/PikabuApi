using Microsoft.EntityFrameworkCore;
using PikabuApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikabuApi
{
    public class PikabuDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public DbSet<PostContent> PostContents { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<CommentAttachment> CommentAttachments { get; set; }

        public DbSet<Community> Communities { get; set; }

        public PikabuDbContext(DbContextOptions<PikabuDbContext> options)
            :base(options)
        {

        }
    }
}

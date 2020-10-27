using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CodingBlog.Models;

namespace CodingBlog.Data
{
    public class ApplicationDbContext : IdentityDbContext<BlogUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CodingBlog.Models.Blog> Blog { get; set; }
        public DbSet<CodingBlog.Models.Post> Post { get; set; }
        public DbSet<CodingBlog.Models.Comment> Comment { get; set; }
        public DbSet<CodingBlog.Models.Tag> Tag { get; set; }
    }
}

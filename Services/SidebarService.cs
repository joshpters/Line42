using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingBlog.Data;
using CodingBlog.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingBlog.Services
{
    public class SidebarService : ISidebarService
    {
        private readonly ApplicationDbContext _context;

        public SidebarService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Blog>> GetBlogs()
        {
            var blogs = _context.Blog.Include(b => b.Posts).Where(b => b.Posts.Where(p => p.IsPublished == true).Count() > 0);
            //var blogs = await _context.Blog.ToListAsync();

            return (await blogs.ToListAsync());
        }

        public async Task<IEnumerable<Post>> GetRecentPosts(int num)
        {
            var posts = _context.Post.Where(p => p.IsPublished == true)
                .OrderByDescending(p => p.Created)
                .Take(num);

            return (await posts.ToListAsync());
        }

        public async Task<IEnumerable<Post>> GetSuggestedPosts(int num, int ignoreId)
        {
            //ignore id so we don't suggest the same post the user is on
            var posts = _context.Post
                .Include(p => p.Blog)
                .Where(p => p.IsPublished == true && p.Id != ignoreId)
                .OrderByDescending(p => p.Comments.Count())
                .Take(num);

            return (await posts.ToListAsync());
        }
    }
}

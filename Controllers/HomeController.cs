using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodingBlog.Models;
using Microsoft.EntityFrameworkCore;
using CodingBlog.Data;
using Microsoft.AspNetCore.Http;
using CodingBlog.ViewModels;

namespace CodingBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Category(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var posts = await _context.Post
                .Include(p => p.Blog)
                .Where(p => p.IsPublished == true && p.BlogId == id)
                .OrderByDescending(p => p.Created)
                .ToListAsync();

            ViewData["BlogId"] = id;

            return View(nameof(Index), new PostIndexVM(posts));
        }

        public async Task<IActionResult> Index(string query, int page = 0)
        {
            var posts = _context.Post
                .Include(p => p.Blog)
                .OrderByDescending(p => p.Created)
                .Where(p => p.IsPublished == true);
                

            if(query != null)
            {
                posts = posts.Where(p => p.Content.ToLower().Contains(query.ToLower()) || p.Title.ToLower().Contains(query.ToLower()));
                ViewData["Query"] = query;
            }

            int allPostCount = posts.Count();
            posts = posts.Skip(page * 4).Take(4);

            PostIndexVM model = new PostIndexVM(await posts.ToListAsync(), allPostCount, page, 4);

            return View(model);
        }
        
        
        //public async Task<IActionResult> Index(int? id, string query)
        //{
        //    var blogs = await _context.Blog.Where(p => p.Posts.Count() > 0).ToListAsync();
        //    if (id != null)
        //    {
        //        var blog = await _context.Blog.FindAsync(id);
        //        if (blog != null)
        //        {
        //            var postsFiltered = await _context.Post.Where(p => p.BlogId == id && p.IsPublished).OrderByDescending(p => p.Created).ToListAsync();
        //            var viewModelFiltered = new PostBlogVM(postsFiltered, blogs);
        //            ViewData["BlogId"] = id;
        //            return View(viewModelFiltered);
        //        }
        //    }

        //    if (query != null)
        //    {
        //        var postsSearchFiltered = await _context.Post.Where(p => p.Content.Contains(query) && p.IsPublished).OrderByDescending(p => p.Created).ToListAsync();
        //        var viewModelSearchFiltered = new PostBlogVM(postsSearchFiltered, blogs);
        //        ViewData["Query"] = query;
        //        return View(viewModelSearchFiltered);
        //    }

        //    var posts = await _context.Post.Where(p => p.IsPublished).OrderByDescending(p => p.Created).ToListAsync();
        //    var viewModel = new PostBlogVM(posts, blogs);

        //    return View(viewModel);
        //}

        //public async Task<IActionResult> BlogPosts(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var blog = await _context.Blog.FindAsync(id);
        //    if (blog == null)
        //    {
        //        return NotFound();
        //    }

        //    ViewData["BlogName"] = blog.Name;
        //    ViewData["BlogId"] = blog.Id;

        //    var blogPosts = await _context.Post.Where(p => p.BlogId == id).ToListAsync();
        //    return View(blogPosts);
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

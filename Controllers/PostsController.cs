using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodingBlog.Data;
using CodingBlog.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using CodingBlog.Helpers;
using CodingBlog.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace CodingBlog.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public object ImageUploader { get; private set; }

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Posts
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var posts = await _context.Post.ToListAsync();
            var blogs = await _context.Blog.ToListAsync();
            var viewModel = new PostBlogVM(posts, blogs);
            return View(viewModel);
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .Include(p => p.Blog)
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (post == null)
            {
                return NotFound();
            }
            foreach (var comment in post.Comments.ToList())
            {
                comment.BlogUser = await _context.Users.FindAsync(comment.BlogUserId);
            }
            //ViewData["Image"] = ImageHelper.DecodeImage(post.Image, post.FileName);

            return View(post);
        }
        //Get: Posts/BlogPosts/BlogId
        public async Task<IActionResult> BlogPosts(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blog.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            ViewData["BlogName"] = blog.Name;
            ViewData["BlogId"] = blog.Id;

            var blogPosts = await _context.Post.Where(p => p.BlogId == id).ToListAsync();
            return View(blogPosts);
        }


        // GET: Posts/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(int? id)
        {
            var post = new Post();
            if (id != null)
            {
                var blog = await _context.Blog.FindAsync(id);
                if (blog != null)
                {
                    post.BlogId = (int)id;
                }
            }

            if (post.BlogId == 0)
            {
                ViewData["BlogId"] = new SelectList(_context.Blog, "Id", "Name");
            } else
            {
                ViewData["BlogId"] = new SelectList(_context.Blog, "Id", "Name", post.BlogId);
            }

            return View(post);
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogId,Title,Content,Abstract,IsPublished")] Post post, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                post.Created = DateTime.Now;
                if (image != null)
                {
                    post.FileName = image.FileName;
                    //var encoder = new ImageUploader();
                    post.Image = ImageHelper.EncodeImage(image);
                }
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["BlogId"] = new SelectList(_context.Blog, "Id", "Id", post.BlogId);
            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["BlogId"] = new SelectList(_context.Blog, "Id", "Name", post.BlogId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BlogId,Title,Content,Abstract,Created,IsPublished")] Post post, IFormFile image)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (image != null)
                    {
                        post.FileName = image.FileName;
                        post.Image = ImageHelper.EncodeImage(image);
                    }
                    else
                    {
                        //var existingPost = _context.Post.Find(id);
                        //post.Image = existingPost.Image;
                    }
                    post.Updated = DateTime.Now;
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogId"] = new SelectList(_context.Blog, "Id", "Name", post.BlogId);
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .Include(p => p.Blog)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Post.FindAsync(id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }
    }
}

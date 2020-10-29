using System.Collections.Generic;
using CodingBlog.Models;

namespace CodingBlog.ViewModels
{
    public class PostBlogVM
    {
        public IList<Post> Posts { get; set; }

        public IList<Blog> Blogs { get; set; }

        public PostBlogVM(IList<Post> posts, IList<Blog> blogs)
        {
            Posts = posts;
            Blogs = blogs;
        }

        public PostBlogVM()
        {

        }
    }
}

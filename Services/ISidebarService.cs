using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingBlog.Models;

namespace CodingBlog.Services
{
    public interface ISidebarService
    {
        Task<IEnumerable<Blog>> GetBlogs();
        Task<IEnumerable<Post>> GetRecentPosts(int num);
        Task<IEnumerable<Post>> GetSuggestedPosts(int num, int ignoreId);
    }
}

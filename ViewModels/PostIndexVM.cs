using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingBlog.Models;

namespace CodingBlog.ViewModels
{
    public class PostIndexVM
    {
        public PostIndexVM()
        {

        }

        public PostIndexVM(IList<Post> posts)
        {
            Posts = posts;
        }
        public PostIndexVM(IList<Post> posts, int postCount, int pageNum, int perPage = 4)
        {
            PostCount = postCount;
            Posts = posts;
            PageNum = pageNum;
            PerPage = perPage;


            if (postCount > (pageNum + 1) * PerPage)
            {
                PagesInFront = true;
            }

            if (pageNum > 0)
            {
                PagesBehind = true;
            }
        }
        public IList<Post> Posts { get; set; }
        public int PerPage { get; set; }
        public int PostCount { get; set; }
        public int PageNum { get; set; }
        public bool PagesInFront { get; set; }
        public bool PagesBehind { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingBlog.Models
{
    public class Blog
    {
        #region Keys
        public int Id { get; set; }

        #endregion

        #region Properties
        public string Name { get; set; }

        public string Url { get; set; }

        #endregion

        #region Navigation

        public virtual ICollection<Post> Posts { get; set; }
        #endregion

        public Blog()
        {
            Posts = new HashSet<Post>();
        }
    }
}

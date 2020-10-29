using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingBlog.Models
{
    public class Comment
    {
        #region Keys
        public int Id { get; set; }

        public int PostId { get; set; }

        public string BlogUserId { get; set; }
        #endregion

        #region Comment Properties
        public string Content { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public bool Approved { get; set; }
        #endregion

        #region Navigation
        public Post Post { get; set; }

        public BlogUser BlogUser { get; set; }
        #endregion

        public Comment()
        {

        }

    }
}

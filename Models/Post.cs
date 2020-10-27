using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;

namespace CodingBlog.Models
{
    public class Post
    {

        #region Keys
        public int Id { get; set; }

        public int BlogId { get; set; }
        #endregion

        #region Post Properties


        public string Title { get; set; }

        public string Content { get; set; }

        public byte[] Image { get; set; }
        [Display(Name = "File Name")]
        public string FileName { get; set; }
        
        [StringLength(300, MinimumLength = 6)]
        public string Abstract { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        #endregion
        public Blog Blog { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public string Slug { get; set; }

        public bool IsPublished { get; set; }
        #region Navigation

        #endregion

        public Post()
        {
            Comments = new HashSet<Comment>();
            Tags = new HashSet<Tag>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspNetInläming1.Models
{
    public partial class Post
    {
        public int PostId { get; set; }
        public int? CategoryId { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public DateTime Date { get; set; }

        public virtual Category? Category { get; set; }
    }
}

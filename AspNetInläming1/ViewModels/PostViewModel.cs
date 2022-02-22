using AspNetInläming1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetInläming1.ViewModels
{
    public class PostViewModel 
    {
        public Post CurrentPost { get; set; }
        public List<Post> Posts { get; set;} = new List<Post>();
        public List<SelectListItem> Categories { get; set; }
        public DateTime PostedDate { get; set; }
        public string Search { get; set; }

    }
}
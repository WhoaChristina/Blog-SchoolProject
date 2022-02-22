using AspNetInläming1.Models;
using AspNetInläming1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetInläming1.Controllers
{
    public class PostController : Controller
    {
        private readonly BlogContext _connString;

        public PostController(BlogContext conn)
        {
            _connString = conn;
        }
        public IActionResult ShowAllPosts()
        {
            PostViewModel model = new PostViewModel();   
            var posts = _connString.Posts.ToList<Post>();
            model.Posts = posts;
            model.Posts.Sort(delegate (Post p1, Post p2) { return p2.Date.CompareTo(p1.Date); });
            return View(model);
        }
        [HttpPost]
        public IActionResult ShowAllPosts(PostViewModel val)
        {

            var res = _connString.Posts.Where(p => p.Title.Contains(val.Search) || p.Category.CategoryName.Contains(val.Search)).ToList();
            val.Posts = res;
            val.Posts.Sort(delegate (Post p1, Post p2) { return p2.Date.CompareTo(p1.Date); });
            return View(val);
        }
        public IActionResult MakePost()
        {
            var model = new PostViewModel();
            var cats = _connString.Categories.ToList();
            var selectList = new List<SelectListItem>();
            foreach (var c in cats)
            {
                selectList.Add(GetSelectListItems(c));
            }
            model.PostedDate = DateTime.Now;
            model.Categories = selectList;
            return View(model);
        }

        public SelectListItem GetSelectListItems(Category cat)
        {
            return new SelectListItem(cat.CategoryName, cat.CategoryId.ToString());
        }

        public Category CovertSelectedListToCategory(List<Category> categories, int idMatch)
        {
            foreach (var item in categories)
            {
                if (item.CategoryId == idMatch)
                {
                    return item;
                }
            }
            return null;
        }

        [HttpPost]
        public IActionResult AddPost(PostViewModel post)
        {
            post.CurrentPost.Date = post.PostedDate;
            var cats = _connString.Categories.ToList();

            post.CurrentPost.Category = CovertSelectedListToCategory(cats, (int)post.CurrentPost.CategoryId);
            post.CurrentPost.Date = DateTime.Now;
            _connString.Posts.Add(post.CurrentPost);
            _connString.SaveChanges();
            
            PostViewModel model = new PostViewModel();
            var posts = _connString.Posts.ToList<Post>();
            model.Posts = posts;
            model.Posts.Sort(delegate (Post p1, Post p2) { return p2.Date.CompareTo(p1.Date); });
            return View("ShowAllPosts", model);
        }

        public IActionResult CloserLook(int id)
        {
            var model = _connString.Posts.Where(p => p.PostId == id).SingleOrDefault();
            var cat = _connString.Categories.Where(p => p.CategoryId == model.CategoryId).SingleOrDefault();
            PostViewModel post = new PostViewModel();
            post.CurrentPost = model;
            post.CurrentPost.Category = cat;
            return View(post);
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using my_blog.Data;
using my_blog.Models;
using my_blog.Repositories;

namespace my_blog.Controllers
{
    public class HomeController : Controller
    {
        private IBlogRepository _repository;
        public HomeController(IBlogRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            var posts = _repository.GetAllPosts();
            return View(posts);
        }

        public IActionResult Post(int id)
        {
            var post = _repository.GetPost(id);
            return View(post);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id == null)
                return View(new Post());
            else {
                var post = _repository.GetPost((int) id);
                return View(post);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            if(post.Id > 0)
                _repository.UpdatePost(post);
            else 
                _repository.AddPost(post);
    
            if(await _repository.SaveChangesAsync())
            {
                return RedirectToAction("Index");
            }
            else{
                return View(post);
            }
        }

        public async Task<IActionResult> Remove(int id)
        {
            _repository.RemovePost(id);
            await _repository.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
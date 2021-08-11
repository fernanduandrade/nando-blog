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
            return View();
        }

        public IActionResult Post()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View(new Post());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            _repository.AddPost(post);
            if(await _repository.SaveChangesAsync())
            {
                return RedirectToAction("Index");
            }
            else{
                return View(post);
            }
        }
    }
}
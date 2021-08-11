using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using my_blog.Data;
using my_blog.Models;

namespace my_blog.Controllers
{
    public class HomeController : Controller
    {
        private DataContext _ctx;
        public HomeController(DataContext ctx)
        {
            _ctx = ctx;
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
            _ctx.Add(post);
            await _ctx.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
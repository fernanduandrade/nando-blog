using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using my_blog.Data;
using my_blog.Data.FileManager;
using my_blog.Models;
using my_blog.Repositories;

namespace my_blog.Controllers
{
    public class HomeController : Controller
    {
        private IBlogRepository _repository;
        private IFileManager _filemanager;
        public HomeController(IBlogRepository repository, IFileManager fileManager)
        {
            _repository = repository;
            _filemanager = fileManager;
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

        [HttpGet("/Image/{image}")]
        public IActionResult Image(string image)
        {
            var mine = image.Substring(image.LastIndexOf('.') + 1);
            return new FileStreamResult(_filemanager.ImageStream(image),$"image/{mine}");
        }
    }
}
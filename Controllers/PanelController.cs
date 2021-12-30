using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using my_blog.Repositories;
using System.Threading.Tasks;
using my_blog.Models;
using my_blog.ViewModels;
using my_blog.Data.FileManager;

namespace my_blog.Controllers
{
    [Authorize(Roles ="Admin")]
    public class PanelController : Controller
    {
        private IBlogRepository _repository;
        private IFileManager _fileManager;
        public PanelController(IBlogRepository repository, IFileManager fileManager)
        {
            _repository = repository;
            _fileManager = fileManager;
        }

        [Route("/panel/admin")]
        public IActionResult Index()
        {
            var posts = _repository.GetAllPosts();
            return View(posts);
        }

        [Route("/post")]
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return View(new PostViewModel());
            }

            else {
                var post = _repository.GetPost((int) id);
                return View(new PostViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    Body = post.Body,
                    CurrentImage = post.Image,
                    Description = post.Description,
                    Category = post.Category,
                    Tags = post.Tags
                });
            }
        }

        [Route("/post")]
        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel viewModel)
        {
            Post post = new Post
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                Body = viewModel.Body,
                Description = viewModel.Description,
                Category = viewModel.Category,
                Tags = viewModel.Tags
                
            };

            if(viewModel.Image == null)
                post.Image = viewModel.CurrentImage;
            else 
            {   if(!string.IsNullOrEmpty(viewModel.CurrentImage))
                    _fileManager.RemoveImage(viewModel.CurrentImage);
                post.Image = await _fileManager.SaveImage(viewModel.Image);
            }
                

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

        [Route("delete-post")]
        public async Task<IActionResult> Remove(int id)
        {
            _repository.RemovePost(id);
            await _repository.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

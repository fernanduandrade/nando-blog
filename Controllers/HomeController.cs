using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using my_blog.Data;
using my_blog.Data.FileManager;
using my_blog.Models;
using my_blog.Repositories;
using my_blog.ViewModels;
using System.Collections.Generic;
using nando_blog.Models.Comments;
using System;

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

        public IActionResult Index(int pageNumber, string category, string search)
        {
            if(pageNumber < 1 )
                return RedirectToAction("Index", new {pageNumber = 1, category});
            
        
            var vm = _repository.GetAllPosts(pageNumber, category, search);
            return View(vm);
        }
            
        [Route("/{id}/{slug}")]
        public IActionResult Post(int id) => View(_repository.GetPost(id));
        
        
        [HttpGet("/Image/{image}")]
        [ResponseCache(CacheProfileName="Monthly")]
        public IActionResult Image(string image) =>
            new FileStreamResult(_filemanager.ImageStream(image),$"image/{image.Substring(image.LastIndexOf('.') + 1)}");


        [HttpPost]
        public async Task<IActionResult> Comment(CommentViewModel viewModel) 
        {
            if(!ModelState.IsValid)
                return RedirectToAction("Post", new {id = viewModel.PostId});

            var post = _repository.GetPost(viewModel.PostId);
            if(viewModel.MainCommentId == 0 )
            {
                post.MainComments = post.MainComments ?? new List<MainComment>();
                post.MainComments.Add(new MainComment
                {
                    Message = viewModel.Message,
                    Created = DateTime.Now,
                });
                _repository.UpdatePost(post);
            }
            else 
            {
                var comment = new SubComment
                {
                    MainCommentId = viewModel.MainCommentId,
                    Message = viewModel.Message,
                    Created = DateTime.Now,
                };

                _repository.AddSubComment(comment);
            }

            await _repository.SaveChangesAsync();

            return RedirectToAction("Post", new {id = viewModel.PostId});
        }

        [HttpGet]
        public IActionResult About() => View();
    }
}

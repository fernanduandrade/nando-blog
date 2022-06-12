using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Data;
using MyBlog.Data.FileManager;
using MyBlog.Models;
using MyBlog.Repositories;
using MyBlog.ViewModels;
using System.Collections.Generic;
using System;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogRepository _repository;
        private readonly IFileManager _filemanager;
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
        public IActionResult Post(int id) 
        {
            var post = _repository.GetPost(id);
            return View(post);
        } 
        
        
        [HttpGet("/Image/{image}")]
        [ResponseCache(CacheProfileName="Monthly")]
        public IActionResult Image(string image) =>
            new FileStreamResult(_filemanager.ImageStream(image),$"image/{image.Substring(image.LastIndexOf('.') + 1)}");


        [Route("/about")]
        [HttpGet]
        public IActionResult About() => View("About");
    }
}

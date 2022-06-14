using MyBlog.Controllers;
using MyBlog.Repositories;
using MyBlog.Data.FileManager;
using Microsoft.AspNetCore.Mvc;
using MyBlog.ViewModels;

namespace MyBlogTest.ControllersTest
{
    public class HomeControllerTest
    {
        private readonly HomeController _controller;
        private readonly Mock<IBlogRepository> _repository;
        private readonly Mock<IFileManager> _fileManager;
        
        public HomeControllerTest()
        {
            _repository = new Mock<IBlogRepository>();
            _fileManager = new Mock<IFileManager>();
            _controller = new HomeController(_repository.Object, _fileManager.Object);
        }
        [Fact]
        public void AboutViewReturnViewName()
        {
            // Act
            var AboutAction = _controller.About() as ViewResult;
            bool result = AboutAction?.ViewName == "About";
            // Assert

            Assert.True(result);
        }

        [Fact]
        public void AboutViewReturnViewResult()
        {
            // Act
            var result = _controller.About() as ViewResult;

            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public void IndexGivenZeroShouldRedirectToIndexView()
        {
            var result = _controller.Index(0, "Programming", "");

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void IndexGivenOneShouldReturnViewResult()
        {
            var result = _controller.Index(1, "Programming", "");

            Assert.IsType<ViewResult>(result);
        }
    }
}

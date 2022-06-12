using Microsoft.AspNetCore.Http;

namespace MyBlog.ViewModels
{
    public class PostViewModel
    {
        public int Id {get; set;}
        public string Title { get; set; } = "";
        public string Body { get; set; } = "";
        public string CurrentImage { get; set; } = "";
        public string Category {get; set;} = "";
        public string Tags {get; set;} = "";
        public string Description {get; set;} = "";
        public string Slug { get; set; } = "";
        public IFormFile Image {get; set;} = null;
    }
}
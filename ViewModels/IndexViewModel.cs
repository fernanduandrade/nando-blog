using System.Collections.Generic;
using my_blog.Models;

namespace my_blog.ViewModels
{
    public class IndexViewModel
    {
        public int PageNumber {get; set;}
        public bool NextPage {get; set;}
        public int PageCount {get; set;}
        public string Category {get; set;}
        public IEnumerable<Post> Posts { get; set; }
    }
}
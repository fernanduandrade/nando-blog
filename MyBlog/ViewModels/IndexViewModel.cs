using System.Collections.Generic;
using MyBlog.Models;

namespace MyBlog.ViewModels
{
    public class IndexViewModel
    {
        internal IEnumerable<int> Pages;
        public string Search {get; set;}
        public int PageNumber {get; set;}
        public bool NextPage {get; set;}
        public int PageCount {get; set;}
        public string Category {get; set;}
        public string Slug { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}
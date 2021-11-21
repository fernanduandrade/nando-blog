using System;
using System.Collections.Generic;

namespace nando_blog.Models.Comments
{
    public class MainComment : Comment
    {
        public List<SubComment> SubComments {get; set;}
    }
}
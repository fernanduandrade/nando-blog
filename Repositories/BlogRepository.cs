using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using my_blog.Data;
using my_blog.Models;

namespace my_blog.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private DataContext _ctx;
        
        public BlogRepository(DataContext ctx)
        {
            _ctx = ctx;
        }

        public void AddPost(Post post)
        {
            _ctx.Add(post);
        }

        public List<Post> GetAllPosts()
        {
            return _ctx.Post.ToList();
        }

        public List<Post> GetAllPosts(string category)
        {
            return _ctx.Post
                .Where(post=> post.Category.ToLower().Equals(category.ToLower()))
                .ToList();
        }

        public Post GetPost(int id)
        {
            return _ctx.Post.FirstOrDefault(post => post.Id == id);
        }

        public void RemovePost(int id)
        {
            _ctx.Post.Remove(GetPost(id));
        }

        public async Task<bool> SaveChangesAsync()
        {
            if(await _ctx.SaveChangesAsync() > 0)
            {
                return true;
            }
            else {
                return false;
            }
        }

        public void UpdatePost(Post post)
        {
            _ctx.Post.Update(post);
        }
    }
}
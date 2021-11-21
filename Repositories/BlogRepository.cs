using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using my_blog.Data;
using my_blog.Models;
using my_blog.ViewModels;
using nando_blog.Models.Comments;

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

        public void AddSubComment(SubComment subComment)
        {
            _ctx.SubComments.Add(subComment);
        }

        public List<Post> GetAllPosts()
        {
            return _ctx.Post
                .ToList();
        }

        public IndexViewModel GetAllPosts(int pageNumber, string category)
        {
            Func<Post, bool> InCategory = (post) => {return post.Category.ToLower().Equals(category.ToLower());};
            int pageSize = 5;
            int skipAmount = pageSize * (pageNumber - 1);
            int capacity = skipAmount * pageSize;
            var query = _ctx.Post.AsQueryable();
            
            if(!string.IsNullOrEmpty(category))
                query = query.Where(x => InCategory(x));

            int postsCount = query.Count();

            return new IndexViewModel
            {
                PageNumber = pageNumber,
                Category = category,
                NextPage = postsCount > capacity,
                Posts = query
                    .Skip(skipAmount)
                    .Take(pageSize)
                    .ToList()
            };
        }

        public Post GetPost(int id)
        {
            return _ctx.Post
            .Include(p => p.MainComments)
                .ThenInclude(mainComments => mainComments.SubComments)
            .FirstOrDefault(post => post.Id == id);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using my_blog.Data;
using my_blog.Models;
using my_blog.ViewModels;
using nando_blog.Models.Comments;
using System.Globalization;

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

        public IndexViewModel GetAllPosts(int pageNumber, string category, string search)
        {
            Func<Post, bool> InCategory = (post) => {return post.Category.ToLower().Equals(category.ToLower());};
            int pageSize = 10;
            int skipAmount = pageSize * (pageNumber - 1);
            var query = _ctx.Post.AsNoTracking().AsQueryable();
            
            if (!String.IsNullOrEmpty(category))
                query = query.Where(x => x.Category.Equals(category));

            if (!String.IsNullOrEmpty(search))
                query = query.Where(x => EF.Functions.Like(x.Title, $"%{search}%")
                                    || EF.Functions.Like(x.Body, $"%{search}%") 
                                    || EF.Functions.Like(x.Description, $"%{search}%"));


            int postsCount = query.Count();
            int pageCount = (int) Math.Ceiling(postsCount * 1.0 / pageSize);
            return new IndexViewModel
            {
                PageNumber = pageNumber,
                PageCount = pageCount,
                Search = search,
                Pages = PageNumbers(pageNumber, postsCount).ToList(),
                Category = category,
                NextPage = postsCount > skipAmount + pageSize,
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
        private IEnumerable<int> PageNumbers(int pageNumber, int pageCount)
        {
            int midPoint = pageNumber < 3 ? 3 : pageNumber > pageCount - 2 ? pageCount -2 : pageNumber;
            int lowerBound = midPoint - 2;
            int upperBound = midPoint + 2;

            if(lowerBound != 1)
            {
                yield return 1;
                if(lowerBound - 1 > 1)
                {
                    yield return -1;
                }
            }

            for(int i = midPoint - 2; i <= midPoint + 2; i++)
            {
                yield return i;
            }

            if(upperBound != pageCount )
            {
                if(pageCount - upperBound > 1)
                {
                    yield return -1;
                }

                yield return pageCount;
            }
        }
    }
}
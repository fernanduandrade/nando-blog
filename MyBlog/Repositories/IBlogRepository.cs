using System.Collections.Generic;
using System.Threading.Tasks;
using MyBlog.Models;
using MyBlog.ViewModels;

namespace MyBlog.Repositories
{
    public interface IBlogRepository
    {
        Post GetPost(int id);
        List<Post> GetAllPosts();
        IndexViewModel GetAllPosts(int pageNumberm, string category, string search);
        void RemovePost(int id);
        void UpdatePost(Post post);
        void AddPost(Post post); 
        Task<bool> SaveChangesAsync();
    }
}
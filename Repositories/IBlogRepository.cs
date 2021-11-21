using System.Collections.Generic;
using System.Threading.Tasks;
using my_blog.Models;
using my_blog.ViewModels;
using nando_blog.Models.Comments;

namespace my_blog.Repositories
{
    public interface IBlogRepository
    {
        Post GetPost(int id);
        List<Post> GetAllPosts();
        IndexViewModel GetAllPosts(int pageNumberm, string category);
        void RemovePost(int id);
        void AddSubComment(SubComment subComment);
        void UpdatePost(Post post);
        void AddPost(Post post); 
        Task<bool> SaveChangesAsync();
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using my_blog.Models;

namespace my_blog.Repositories
{
    public interface IBlogRepository
    {
        Post GetPost(int id);
        List<Post> GetAllPosts(int id);
        void RemovePost(int id);
        void UpdatePost(Post post);
        void AddPost(Post post); 
        Task<bool> SaveChangesAsync();
    }
}
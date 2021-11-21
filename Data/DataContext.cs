using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using my_blog.Models;
using nando_blog.Models.Comments;

namespace my_blog.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Post> Post {get; set;}
        public DbSet<MainComment> MainComments {get; set;}
        public DbSet<SubComment> SubComments {get; set;}
    }
}

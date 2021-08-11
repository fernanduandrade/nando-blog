using Microsoft.EntityFrameworkCore;
using my_blog.Models;

namespace my_blog.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Post> Post {get; set;}
    }
}
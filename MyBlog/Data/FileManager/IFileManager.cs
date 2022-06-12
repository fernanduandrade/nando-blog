using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyBlog.Data.FileManager
{
    public interface IFileManager
    {
        FileStream ImageStream(string image);
        Task<string> SaveImage(IFormFile image);
        bool RemoveImage(string image);
    }
}
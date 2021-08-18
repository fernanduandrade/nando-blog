using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;
using System;

namespace my_blog.Data.FileManager
{
    public class FileManager : IFileManager
    {
        private string _imagePath;
        public FileManager(IConfiguration config)
        {
            _imagePath = config["Images"];
        }

        public FileStream ImageStream(string image)
        {
            return new FileStream(Path.Combine(_imagePath, image), FileMode.Open, FileAccess.Read);
        }

        public async Task<string> SaveImage(IFormFile image)
        {
            try
            {
                var savePath = Path.Combine(_imagePath);
                if(!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                var mine = image.FileName.Substring(image.FileName.LastIndexOf('.'));
                var fileName = $"img_{DateTime.Now.ToString("dd-MM-yyyy")}{mine}";
                
                using(var fileStream = new FileStream(Path.Combine(savePath, fileName), FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                return fileName;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return "não salvou pq deu erro no fileStream :(";
            }
            
        }
    }
}
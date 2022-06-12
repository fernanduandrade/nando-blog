using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;
using System;
using PhotoSauce.MagicScaler;

namespace MyBlog.Data.FileManager
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

        public bool RemoveImage(string image)
        {
            try
            {
                var file =  Path.Combine(_imagePath, image);
                if(File.Exists(file))
                    File.Delete(file);
                        
                return true;
            }
            catch (Exception)
            {
                
                return false;
            }
            
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
                    // await image.CopyToAsync(fileStream);
                    MagicImageProcessor.ProcessImage(image.OpenReadStream(),  fileStream, ImageOptions());
                }
                return fileName;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return "não salvou pq deu erro no fileStream :(";
            }
            
        }

        private ProcessImageSettings ImageOptions() => new ProcessImageSettings
        {
            Width= 800,
            Height = 50,
            ResizeMode = CropScaleMode.Crop,
            SaveFormat = FileFormat.Jpeg,
            JpegQuality = 100,
            JpegSubsampleMode = ChromaSubsampleMode.Subsample420
        };
    }
}
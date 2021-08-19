using System.ComponentModel.DataAnnotations;

namespace my_blog.ViewModels
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
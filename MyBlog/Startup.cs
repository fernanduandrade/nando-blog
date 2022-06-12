using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MyBlog.Data;
using MyBlog.Repositories;
using Microsoft.AspNetCore.Identity;
using MyBlog.Data.FileManager;

namespace MyBlog
{
    public class Startup
    {
        private IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options => options.UseNpgsql(_config.GetConnectionString("DefaultConnection")));
            services.AddDbContext<DataContext>();
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<DataContext>();
            
            services.ConfigureApplicationCookie(option => {
                option.LoginPath = "/Auth/Login";
            });

            services.AddTransient<IBlogRepository, BlogRepository>();
            services.AddTransient<IFileManager, FileManager>();
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddMvc(options => 
            {
                options.CacheProfiles.Add("Daily", new Microsoft.AspNetCore.Mvc.CacheProfile {Duration = 60 * 60 * 24 * 7 * 4});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using my_blog.Data;
using my_blog.Repositories;
using Microsoft.AspNetCore.Identity;
using my_blog.Data.FileManager;

namespace my_blog
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //}
            //    app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapGet("/", async context =>
            //     {
            //         await context.Response.WriteAsync("Hello World!");
            //     });
            // });
        }
    }
}

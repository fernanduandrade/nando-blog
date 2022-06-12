using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using my_blog.Data;

namespace my_blog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            
            try {
                IServiceScope scope =  host.Services.CreateScope();    
                var ctx = scope.ServiceProvider.GetRequiredService<DataContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                
                ctx.Database.EnsureCreated();

                var adminRole = new IdentityRole("Admin");
                if(!ctx.Roles.Any())
                {
                    // Criar um cargo
                    roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
                }

                
                if(!ctx.Users.Any(user => user.UserName == "admin"))
                {
                    // Criar um user admin se não existir
                    var adminUser = new IdentityUser
                    {
                        UserName = "admin",
                        Email = "nando.jua@gmail.com"
                    };

                    userManager.CreateAsync(adminUser, "Juazeiro0.").GetAwaiter().GetResult();

                    // Passar a função de administrador para meu usuário 

                    userManager.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();

                }
            } 
            catch(Exception error)
            {
                Console.WriteLine(error.Message);
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

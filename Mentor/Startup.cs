using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mentor.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Mentor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(connection));

            services.AddIdentity<User, IdentityRole>(opts => {
                //opts.Password.RequiredLength = 5;   // ����������� �����
                //opts.Password.RequireNonAlphanumeric = false;   // ��������� �� �� ���������-�������� �������
                //opts.Password.RequireLowercase = false; // ��������� �� ������� � ������ ��������
                //opts.Password.RequireUppercase = false; // ��������� �� ������� � ������� ��������
                //opts.Password.RequireDigit = false; // ��������� �� �����


                opts.User.RequireUniqueEmail = true;    // ���������� email
     //           opts.User.AllowedUserNameCharacters = ".@abcdefghijklmnopqrstuvwxyz"; // ���������� �������
            })
                .AddEntityFrameworkStores<DataBaseContext>();


            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

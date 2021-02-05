using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kulv.YCF.API.Attributes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NSql2Table.Filters;
using YDal.EntityFramework;

namespace NSql2Table
{
    public class Startup
    {
        readonly string CorsPolicy1 = "MyPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddDal();//��ʽ����Ҫע����δ���������

            services.AddDal().AddDalCodeGenerator();//��ʽ����Ҫע����δ���������

            services.AddCors(options =>
            {
                options.AddPolicy(name: CorsPolicy1,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost",
                            "http://localhost:80",
                            "https://cors1.azurewebsites.net",
                            "https://cors3.azurewebsites.net",
                            "https://localhost:44398",
                            "https://localhost:5001")
                                .WithMethods("PUT", "DELETE", "GET").AllowAnyHeader().AllowCredentials();
                        //builder
                        //.AllowAnyOrigin()
                        //.WithMethods("POST", "DELETE", "GET")
                        //.AllowAnyHeader()
                        //.AllowCredentials(); 
                    });
            });

            Action<MvcOptions> filters = new Action<MvcOptions>(r =>
            {
                r.Filters.Add(typeof(WrapperBodyAttribute));
            });

            services.AddControllers(filters);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseMiddleware<CorsMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            //�������
            app.UseCors(CorsPolicy1);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Default",
                    pattern: "api/{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}

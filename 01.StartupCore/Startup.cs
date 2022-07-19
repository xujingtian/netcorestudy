using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static StartupCore.Services.ScopedDisposableServices;

namespace StartupCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Console.WriteLine("4.3、执行方法：Startup");
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("4.4、执行方法：ConfigureServices");

            /*
             * 服务注册方法 https://docs.microsoft.com/zh-cn/dotnet/core/extensions/dependency-injection#service-registration-methods
             * 服务释放 https://docs.microsoft.com/zh-cn/dotnet/core/extensions/dependency-injection-guidelines#disposal-of-services
             */
            #region 01.ScopedDisposableController  
            /*
             * Add{LIFETIME}<{SERVICE}, {IMPLEMENTATION}>()
             * Add{LIFETIME}<{SERVICE}>(sp => new {IMPLEMENTATION}) （可传参数）
             * Add{LIFETIME}<{IMPLEMENTATION}>()
             */
            #region 暂时/瞬时
            //services.AddTransient<IOrderService, DisposableOrderService>();
            //services.AddTransient<IOrderService>(sp=>new DisposableOrderService());
            /*//services.AddTransient(new DisposableOrderService());//public static IServiceCollection AddTransient(this IServiceCollection services, Type serviceType); 此方法注册不成功*/
            #endregion

            #region 作用域/范围 
            //services.AddScoped<IOrderService, DisposableOrderService>();
            //services.AddScoped<IOrderService>(sp => new DisposableOrderService());
            /*//services.AddScoped(new DisposableOrderService());*/
            #endregion

            #region 单例
            //services.AddSingleton<IOrderService, DisposableOrderService>();
            //services.AddSingleton<IOrderService>(sp=>new DisposableOrderService());
            services.AddSingleton<IOrderService>(new DisposableOrderService());
            /*//services.AddSingleton(new DisposableOrderService());*/
            #endregion

            #endregion
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Console.WriteLine("5、执行方法：Configure");


            //从根容器获取瞬时服务
            //var iorderService1 = app.ApplicationServices.GetService<IOrderService>();
            //var iorderService2 = app.ApplicationServices.GetService<IOrderService>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                //endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

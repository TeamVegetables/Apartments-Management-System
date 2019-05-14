using AMS.Core.Config;
using AMS.Core.Helpers;
using AMS.Core.Interfaces;
using AMS.Core.Models;
using AMS.Core.Repositories;
using AMS.Services.Interfaces;
using AMS.Services.Services;
using AMS.Web.Profiles;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AMS.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("AMS.Core")));
            services.AddDefaultIdentity<User>()
                .AddRoles<IdentityRole>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });

            services.Configure<DevelopmentSettings>(options =>
                Configuration.GetSection("DevelopmentSettings").Bind(options));
          
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            AddApplicationServices(services);

            AddAutoMapper(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DevelopmentDefaultData developmentDefaultData)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

                developmentDefaultData.CreateIfNoExist().Wait();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void AddApplicationServices(IServiceCollection services)
        {
            services.AddTransient<DevelopmentDefaultData>();
            services.AddTransient<BaseService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IApartmentRepository, ApartmentRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<IRequestRepository, RequestRepository>();
            services.AddTransient<IRentInfoRepository, RentInfoRepository>();
            services.AddTransient<IApartmentService, ApartmentService>();
            services.AddTransient<IRequestService, RequestService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<INotificationService, NotificationService>();
        }

        private void AddAutoMapper(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new RequestProfile(serviceProvider.GetService<UserManager<User>>()));
            });

            services.AddSingleton(Mapper.Instance);
        }
    }
}

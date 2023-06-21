using MainConf.Models;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;

namespace MainConf
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
        
            services.AddDbContext<ApplicationContext>(options =>
                          options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<DataContext>(options =>
                         options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);
            services.AddDbContext<ListeningContext>(options =>
                         options.UseNpgsql(Configuration.GetConnectionString("ListeningConnection")), ServiceLifetime.Scoped);
            services.AddDbContext<ReadingContext>(options =>
                         options.UseNpgsql(Configuration.GetConnectionString("ReadingConnection")), ServiceLifetime.Scoped);
            services.AddDbContext<WritingContext>(options =>
                         options.UseNpgsql(Configuration.GetConnectionString("WritingConnection")), ServiceLifetime.Scoped);  
            services.AddDbContext<SpeakingContext>(options =>
                         options.UseNpgsql(Configuration.GetConnectionString("SpeakingConnection")), ServiceLifetime.Scoped);
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
     .AddCookie(options => //CookieAuthenticationOptions
        {
            options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        });
           

            services.AddIdentity<ApplicationUser, IdentityRole>(opts => {
                opts.Password.RequiredLength = 14;   // минимальная длина
                opts.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
                opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
                opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
                opts.Password.RequireDigit = false; // требуются ли цифры
                
            }).AddEntityFrameworkStores<ApplicationContext>();
           /* services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();*/
            services.AddControllersWithViews();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddControllersWithViews()
                .AddDataAnnotationsLocalization()
                .AddViewLocalization();// добавляем локализацию представлений;
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("uz"),

                    new CultureInfo("ru")
                };

                options.DefaultRequestCulture = new RequestCulture("uz");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseRequestLocalization();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //face -api uchun qo'shildi.
            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true,
                DefaultContentType = "text/plain"
            });
            app.UseAuthentication();    // подключение аутентификации
            app.UseAuthorization();
            app.UseRouting();
           
            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Privacy}/{id?}");
           
            });
        }
    }
}

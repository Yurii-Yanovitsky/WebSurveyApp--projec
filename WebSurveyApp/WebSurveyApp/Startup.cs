using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebLogic.Services;
using WebLogic;

namespace WebSurveyApp
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<SurveyDbContext>(options => options.UseSqlServer(_config.GetConnectionString("DefaultConnection")));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option => option.LoginPath = new PathString("/account/login"));
            services.AddLogicServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>

            {
                endpoints.MapControllerRoute("default2", "{controller}/{action}");
                endpoints.MapControllerRoute("default1", "{controller=home}/{action=List}");
                endpoints.MapControllerRoute("default3", "{surveyId}/{controller=takesurvey}/{action=start}");
            });
        }
    }

    public static class ServiceExtensions
    {
        public static IServiceCollection AddLogicServices(this IServiceCollection services)
        {
            return services.AddScoped<AccountService>()
                 .AddScoped<SurveyService>()
                 .AddScoped<QuestionService>()
                 .AddScoped<OptionService>()
                 .AddScoped<ReportService>()
                 .AddScoped<UserService>();
        }
    }
}

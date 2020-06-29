using DataAccess;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using ProjectSettings;

namespace ReaderSphere
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.TryAddSingleton<IGenericReadersphereRepository<Book>, ReaderDataRepository<Book>>();
            services.TryAddSingleton<IGenericReadersphereRepository<Author>, ReaderDataRepository<Author>>();
            services.TryAddSingleton<IGenericReadersphereRepository<BookAuthor>, ReaderDataRepository<BookAuthor>>();
            services.TryAddSingleton<IGenericReadersphereRepository<UserReview>, ReaderDataRepository<UserReview>>();
            services.TryAddSingleton<IConnections, Connections>();
            services.TryAddSingleton<ReaderSphereContext>();
            services.TryAddSingleton<IAppLogger, AppLogger>();
            services.AddHealthChecks().AddCheck<DBHealthCheckProvider>("Database health check");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}"
                    );
            });
        }
    }
}

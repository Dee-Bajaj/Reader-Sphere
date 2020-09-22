using DataAccess;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProjectSettings;
using Models;

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
            services.TryAddSingleton<IBookService, BookService>();
            services.TryAddSingleton<IBookRepository, BookRepository>();
            services.AddHealthChecks().AddCheck<DBHealthCheckProvider>("Database health check");
            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Book Review API",
                    Description = "A simple example ASP.NET Core Web API",
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Book Review API V1");
            });
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

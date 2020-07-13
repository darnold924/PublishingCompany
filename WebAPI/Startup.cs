using DAL.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DAL;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //DAL
            services.AddTransient<IDaoAuthor, DaoAuthor>();
            services.AddTransient<IDaoPayroll, DaoPayroll>();
            services.AddTransient<IDaoArticle, DaoArticle>();

            services.AddDbContext<PublishingCompanyContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            // Add framework services.
            services.AddMvc(options =>
            {
                options.Filters.Add(new ErrorHandlingFilter(Configuration));

            });

            services.AddMvc();
        }
  
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,  ILoggerFactory loggerfactory, IApplicationLifetime appLifetime)
        {
            //this is used by Postmon and Global Exception handelling to show exception.
            app.UseExceptionHandler(
                options =>
                {
                    options.Run(
                        async context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            context.Response.ContentType = "text/html";
                            var ex = context.Features.Get<IExceptionHandlerFeature>();
                            if (ex != null)
                            {
                                var err = $"<h1>Error: {ex.Error.Message} </h1>{ex.Error.StackTrace } {ex.Error.InnerException.Message} ";
                                await context.Response.WriteAsync(err).ConfigureAwait(false);
                            }
                        });
                }
            );

            loggerfactory.AddSerilog();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

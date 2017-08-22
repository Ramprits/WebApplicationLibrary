using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApplicationLibrary.Entities;
using WebApplicationLibrary.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Diagnostics;
using NLog.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using WebApplicationLibrary.Data.Entities;
using WebApplicationLibrary.Data.Service;

namespace WebApplicationLibrary
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["connectionStrings:libraryDBConnectionString"];
            services.AddDbContext<LibraryContext>(o => o.UseSqlServer(connectionString));

            var connectionNORTHWNDContextString = Configuration["connectionStrings:connectionNORTHWNDContextString"];
            services.AddDbContext<NORTHWNDContext>(o => o.UseSqlServer(connectionNORTHWNDContextString));

            services.AddAutoMapper();

            

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddScoped<IUrlHelper>(implementationFactory =>
            {
                var actionContext = implementationFactory.GetService<IActionContextAccessor>()
                .ActionContext;
                return new UrlHelper(actionContext);
            });
            services.AddScoped<ILibraryRepository, LibraryRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddMvc()
            .AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling =
                  ReferenceLoopHandling.Ignore;
            });
            services.AddCors(cfg =>
            {
                cfg.AddPolicy("MyApplication", bldr =>
                {
                    bldr.AllowAnyHeader()
                        .AllowAnyMethod();
                });

                cfg.AddPolicy("AnyGET", bldr =>
                {
                    bldr.AllowAnyHeader()
                        .WithMethods("GET")
                        .AllowAnyOrigin();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory, LibraryContext libraryContext)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug(LogLevel.Information);

            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (exceptionHandlerFeature != null)
                        {
                            var logger = loggerFactory.CreateLogger("Globle Exception logger");
                            logger.LogError(500, exceptionHandlerFeature.Error, exceptionHandlerFeature.Error.Message);

                        }
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");

                    });
                });
            }

            libraryContext.EnsureSeedDataForContext();

            app.UseMvc();
        }
    }
}

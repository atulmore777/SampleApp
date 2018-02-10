using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Bijankur.DAL;
using Bijankur.BL.Services;
using Bijankur.DAL.Repository;
using Swashbuckle.Swagger;
using Swashbuckle.Swagger.Model;

namespace Bijankur.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddEntityFrameworkSqlServer()
                    .AddDbContext<DataLayerContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddMvc();

            services.AddTransient<IErrorMessageService, ErrorMessageService>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IErrorMessageRepository, ErrorMessageRepository>();
            services.AddTransient<IUserRepository, UserRepository>();



            services.AddSwaggerGen(options => {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Auth0 Swagger Sample API",
                    Description = "API Sample made for Auth0",
                    TermsOfService = "None"
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();




            /*Enabling swagger file*/
            app.UseSwagger();
            /*Enabling Swagger ui, consider doing it on Development env only*/
            app.UseSwaggerUi();
        }
    }
}

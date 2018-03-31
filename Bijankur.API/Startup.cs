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
using BJK.DAL;
using BJK.BL.Services;
using BJK.DAL.Repository;
using Swashbuckle.Swagger;
using Swashbuckle.Swagger.Model;
using Serilog;
using BJK.BL.Security;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BJK.API.Common;

namespace BJK.API
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

            Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Debug()
             .WriteTo.RollingFile(System.IO.Path.Combine(Configuration["Logging:LogFile:Path"], "log-{Date}.txt"))
             .CreateLogger();
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

        
            services.Configure<JWTSettings>(Configuration.GetSection("JWTSettings"));

            services.AddTransient<ISecurityHelper, SecurityHelper>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<IErrorMessageService, ErrorMessageService>();

           
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IMenuRepository, MenuRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IErrorMessageRepository, ErrorMessageRepository>();

            services.AddCors();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                       .AllowAnyMethod()
                       .AllowAnyOrigin()
                       .AllowCredentials()
                       .AllowAnyHeader()
                       .WithExposedHeaders("accept", "content-type", "authorization", "x-experience-api-version")
                       .WithHeaders("accept", "content-type", "authorization", "apptoken", "usertoken")
                       .SetPreflightMaxAge(TimeSpan.FromSeconds(3600));
                    });
            });

            services.AddSwaggerGen(options => {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Auth0 Swagger Sample API",
                    Description = "API Sample made for Auth0",
                    TermsOfService = "None"
                });
                options.OperationFilter<AddAuthTokenHeaderParameter>();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(minLevel: LogLevel.Error);
            loggerFactory.AddSerilog();

            // secretKey contains a secret passphrase only your server knows
            var secretKey = Configuration.GetSection("JWTSettings:SecretKey").Value;
            var issuer = Configuration.GetSection("JWTSettings:Issuer").Value;
            var audience = Configuration.GetSection("JWTSettings:Audience").Value;
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = issuer,

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = audience
            };
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                TokenValidationParameters = tokenValidationParameters
            });


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

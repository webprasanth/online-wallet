using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OnlineWallet.Core.Repositories;
using OnlineWallet.Infrastructure.Mappers;
using OnlineWallet.Infrastructure.Repositories;
using OnlineWallet.Infrastructure.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using NLog.Extensions.Logging;
using NLog.Web;
using OnlineWallet.Core;
using OnlineWallet.Infrastructure.Data;
using OnlineWallet.Infrastructure.IoC.Modules;
using OnlineWallet.Infrastructure.Settings;
using OnlineWallet.UI.Framework;
using Swashbuckle.AspNetCore.Swagger;

namespace OnlineWallet.UI
{
    public class Startup
    {

        private readonly IConfigurationRoot _config;
        private readonly IHostingEnvironment _env;
        public IContainer ApplicationContaianer { get; private set; }


        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json");

            _config = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionService, TransactionService>();

            services.AddScoped<IDashboardService, DashboardService>();

            services.AddSingleton<IJwtService, JwtService>();
            services.AddMemoryCache();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Simple docs", Version = "v1" });

                //Set the comments path for the swagger json and ui.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "api.xml");
                c.IncludeXmlComments(xmlPath);
            });

            var builder = new ContainerBuilder();

            if (_env.IsDevelopment())
            {
                services.AddDbContext<OnlineWalletContext>(options => options.UseSqlServer(_config["ConnectionStrings:LocalMSSQL"]));
                builder.RegisterModule(new QueriesModule(_config["ConnectionStrings:LocalMSSQL"]));
                services.Configure<JwtSettings>(_config.GetSection("Jwt"));
            }
            else
            {
                services.AddDbContext<OnlineWalletContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("SQLCONNSTR_Azure")));
                builder.RegisterModule(new QueriesModule(Environment.GetEnvironmentVariable("SQLCONNSTR_Azure")));
                services.Configure<JwtSettings>(o =>
                {
                    o.Issuer = Environment.GetEnvironmentVariable("Issuer");
                    o.ExpiryMinutes = Environment.GetEnvironmentVariable("ExpiryMinutes");
                    o.Key = Environment.GetEnvironmentVariable("Key");
                });
            }

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton(AutoMapperConfig.Initialize());


            services.AddAuthorization();
            services.AddCors(options => options.AddPolicy("default", new CorsPolicy()
            {
                SupportsCredentials = true
            }));
            services.AddMvc();

            builder.Populate(services);
            builder.RegisterModule<CommandModule>();

            ApplicationContaianer = builder.Build();
            return new AutofacServiceProvider(ApplicationContaianer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IApplicationLifetime appLifetime, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole();
            //loggerFactory.AddDebug();
            loggerFactory.AddNLog();
            app.AddNLogWeb();
            _env.ConfigureNLog("nlog.conf");

            JwtSettings jwtSettings;
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                jwtSettings = _config.GetSection("Jwt").Get<JwtSettings>();
            }
            else
            {
                app.UseCustomExceptionHandler();
                jwtSettings = new JwtSettings()
                {
                    ExpiryMinutes = Environment.GetEnvironmentVariable("ExpiryMinutes"),
                    Issuer = Environment.GetEnvironmentVariable("Issuer"),
                    Key = Environment.GetEnvironmentVariable("Key")
                };
            }

            app.UseStaticFiles();

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = false
                }
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = "Cookie",
                LoginPath = new PathString("/Account/Unauthorized/"), //TO DO
                LogoutPath = new PathString("/Account/Logout"),
                AccessDeniedPath = new PathString("/Home/Forbidden/"), //TO DO
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 401;
                            return Task.FromResult<object>(null);
                        }
                        ctx.Response.Redirect(ctx.RedirectUri);
                        return Task.FromResult<object>(null);
                    }
                }
            });


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new {controller = "Home", action = "Index"}
                    );
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            });

            // To dispose of resources that have been resolved in the application container, register for the "ApplicationStopped" event.
            appLifetime.ApplicationStopped.Register(() => ApplicationContaianer.Dispose());

        }
    }
}

using System;
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
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NLog.Extensions.Logging;
using NLog.Web;
using OnlineWallet.Core;
using OnlineWallet.Infrastructure.Data;
using OnlineWallet.Infrastructure.IoC.Modules;
using OnlineWallet.Infrastructure.Settings;
using OnlineWallet.UI.Framework;
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
                .AddJsonFile("config.json");

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

            var builder = new ContainerBuilder();

            if (_env.IsDevelopment())
            {
                services.AddDbContext<OnlineWalletContext>(options => options.UseSqlServer(_config["ConnectionStrings:LocalMSSQL"]));
                builder.RegisterModule(new QueriesModule(_config["ConnectionStrings:LocalMSSQL"]));
        }
            else
            {
                services.AddDbContext<OnlineWalletContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("SQLCONNSTR_Azure")));
                builder.RegisterModule(new QueriesModule(Environment.GetEnvironmentVariable("SQLCONNSTR_Azure")));
            }

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton(AutoMapperConfig.Initialize());

            services.AddAuthorization();

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

            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseCustomExceptionHandler();
            }

            app.UseStaticFiles();

            var jwtSettings = new JwtSettings();
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


            // To dispose of resources that have been resolved in the application container, register for the "ApplicationStopped" event.
            appLifetime.ApplicationStopped.Register(() => ApplicationContaianer.Dispose());

        }
    }
}

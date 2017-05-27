using System;
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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OnlineWallet.Infrastructure.IoC.Modules;
using OnlineWallet.UI.Framework;

namespace OnlineWallet.UI
{
    public class Startup
    {

        public IConfigurationRoot Configuration { get; set; }
        public IContainer ApplicationContaianer { get; private set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionService, TransactionService>();

            services.AddSingleton(AutoMapperConfig.Initialize());

            services.AddAuthorization();

            services.AddMvc();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule<CommandModule>();

            ApplicationContaianer = builder.Build();

            return new AutofacServiceProvider(ApplicationContaianer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomExceptionHandler();

            app.UseStaticFiles();

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = "Cookie",
                LoginPath = new PathString("/Account/Unauthorized/"), //TO DO
                LogoutPath = new PathString("/Account/Logout"),
                AccessDeniedPath = new PathString("/Home/Forbidden/"), //TO DO
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
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

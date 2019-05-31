using System;
using System.IO;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nibo.Prova.Asp.Web.Presentation.Application.CommandHandlers.Transactions;
using Nibo.Prova.Asp.Web.Presentation.Application.CommandHandlers.Transactions.Commands;
using Nibo.Prova.Asp.Web.Presentation.Application.EventHandlers.Transactions;
using Nibo.Prova.Asp.Web.Presentation.Application.EventHandlers.Transactions.Events;
using Nibo.Prova.Asp.Web.Presentation.Application.Notifications;
using Nibo.Prova.Asp.Web.Presentation.Application.Queries;
using Nibo.Prova.Asp.Web.Presentation.Application.Queries.Transactions;
using Nibo.Prova.Domain.Models.Transactions.Repository;
using Nibo.Prova.Infrastructure;
using Nibo.Prova.Infrastructure.Repositories;

namespace Nibo.Prova.Asp.Web.Presentation
{
    public class Startup
    {
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(hostingEnvironment.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true);

                builder.AddEnvironmentVariables();
                Configuration = builder.Build();
            }
            catch (Exception e)
            {
                Log(e);
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            services.AddMediatR(typeof(Startup).Assembly);

            services.AddHttpContextAccessor();

            services.AddDbContext<NiboDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            //Events
            services.AddScoped<INotificationHandler<DeleteAllFilesEvent>, TransactionEventHandler>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            //Command
            services.AddScoped<IRequestHandler<PrecessFileOfxCommand, bool>, TransactionCommandHandler>();

            //Repositories
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            //Reading in the database
            services.AddSingleton<IConnectionStringProvider>(_ => new ConnectionStringProvider(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ITransactionQueries, TransactionQueries>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void Log(Exception e)
            => File.AppendAllText($"Log\\Log{DateTime.Now:yyyy-MM-dd HH-mm-ss}.txt", e.ToString());

    }
}

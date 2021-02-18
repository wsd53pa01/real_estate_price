using System;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RealEstatePrice.Api.Filters;
using RealEstatePrice.Api.Middlewares;
using RealEstatePrice.Autofac;
using RealEstatePrice.Core;
using RealEstatePrice.Service.Interfaces;

namespace real_estate_price
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options => 
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddControllersWithViews(options => 
            {
                options.Filters.Add<ValidateModelFilter>();
            });
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "../RealEstatePrice.ClientApp/dist";
            });

             // Inject appsettings:ConnectionStrings
            IConfigurationSection connectionStrings = Configuration.GetSection("ConnectionStrings");
            services.Configure<ConnectionStrings>(connectionStrings);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Real Estate Price API", Version = "v1"});
                // Set the comments path for the Swagger JSON and UI.
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // add hangfire
            services.AddHangfire(config =>
                    config.UseSimpleAssemblyNameTypeSerializer()
                          .UseRecommendedSerializerSettings()
                          .UseMemoryStorage());

        }

        // autofac configure container
        public void ConfigureContainer(ContainerBuilder builder) 
        {
            builder.LoadContainer("RealEstatePrice.*.dll");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMiddleware<LogMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseHangfireServer();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] {new HangfireAuthorizationFilter()}
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Real Estate Price V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "../RealEstatePrice.ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            RecurringJob.AddOrUpdate<IRealEstatePriceService>("Job：每天早上6點抓取內政部不動產交易 Open Data",
                x => x.FetchRealEstatePrice(),
                "0 6 * * *",
                TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time"));
        }
    }
}

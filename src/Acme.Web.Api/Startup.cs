using Acme.Caching;
using Acme.ChangeHandlers;
using Acme.Data;
using Acme.Muators;
using Acme.Web.Api.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace Acme.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Acme.Web");
                c.RoutePrefix = string.Empty;
#if DEBUG
                c.HeadContent = @"<script>
                    var xmlHttp = new XMLHttpRequest();
                    xmlHttp.open('GET', '/api/setup', true);
                    xmlHttp.send(null);
                    </script>"; // call to set up test data, remove from production
#endif
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDataServices();
            services.AddCaching();
            services.AddDataMutatorsServices();
            services.AddChangeTrackerServices();
            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });
            services.AddSwaggerGen(c =>
            {
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "Acme.Web.Api.xml");
                c.IncludeXmlComments(xmlPath);
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Acme.Web", Version = "v1" });
            });

            var acf = new ConfigurationFactory();
            services.AddSingleton<IApplicationConfiguration>(acf.ApplicationConfig);
        }
    }
}
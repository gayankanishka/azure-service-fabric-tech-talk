﻿using AzureServiceFabric.TechTalk.Ingest.API.Business;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using AzureServiceFabric.TechTalk.Ingest.Core;

namespace AzureServiceFabric.TechTalk.Ingest.API
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
            // TODO: get from config
            string storageAccountKey = "UseDevelopmentStorage=true;";
            string queuename = "messagequeue";

            services.AddMvc()
                .AddApplicationPart(typeof(ApiServiceAssembly).GetTypeInfo().Assembly)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Service Fabric Ingest API",
                    Description = "This is a simple ASP.NET Core web API to showcase Azure service fabric",
                    Contact = new Contact
                    {
                        Name = "Randheer and Gayan",
                        Url = "https://github.com/gayankanishka/azure-service-fabric-tech-talk"
                    }
                });
            });

            ICloudStorage cloudStorage = new CloudStorage(storageAccountKey);
            cloudStorage.CreateQueueIfNotFoundAsync(queuename);
            
            services
                .AddSingleton(cloudStorage)
                .AddScoped<IIngestBusiness, IngestBusiness>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service Fabric Ingest API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
}
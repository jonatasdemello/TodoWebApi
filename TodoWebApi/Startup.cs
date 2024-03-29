﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using TodoWebApi.Repository;
//using Microsoft.OpenApi.Models;

namespace TodoWebApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                //c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                //{
                //    Description = "Standard Authorization. Example: \"Authorization {token}\"",
                //    In = "header",
                //    Name = "Authorization",
                //    Type = "apiKey"
                //});
                //c.OperationFilter<SecurityRequirementsOperationFilter>();
                //var security = new Dictionary<string, IEnumerable<string>>
                //    {
                //        {"Bearer", new string[] { }},
                //    };
                //c.AddSecurityRequirement(security);
            });

            // setup EF
            //services.AddDbContext<TodoContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TodoContext")));
            // To use EF the in-memory database:
            services.AddDbContext<TodoContext>(options => options.UseInMemoryDatabase("TodoWebApiDB"));

            services.AddTransient<ITodoRepository, TodoRepository>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
                c.DocumentTitle = "ToDo API";
                c.DocExpansion(DocExpansion.List); // None, List, Full
                //c.InjectStylesheet("/swagger-ui/custom.css");
                //c.InjectJavascript("/swagger-ui/custom.js");
                c.EnableFilter();
                c.ShowExtensions();
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

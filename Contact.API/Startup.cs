using Contact.API.Infrastructure.Repository.Command;
using Contact.API.Infrastructure.Repository.Command.Base;
using Contact.API.Infrastructure.Repository.Query;
using Contact.API.Infrastructure.Repository.Query.Base;
using ContactAPI.Core.Repositories.Command.Base;
using ContactAPI.Core.Repositories.Query;
using ContactAPI.Core.Repositories.Query.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using MediatR;
using System.Reflection;
using Contact.API.Infrastructure.Data;
using ContactAPI.Application.Handlers.QueryHandler;
using ContactAPI.Application.Commands;
using ContactAPI.Application.Response;
using ContactAPI.Application.Handlers.CommandHandler;
using ContactAPI.Application.Queries;
using ContactAPI.Core.Repositories.Command;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using ContactAPI.Core.Models.Base;
using Newtonsoft.Json;
using LoggerLibrary;
using Contact.API.Infrastructure.Caching.Config;

namespace Contact.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public static string GetAppSettingsParameter(string Key)
        {
            IConfiguration configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", true, true)
           .Build();
            return configuration.GetSection("AppConfiguration").GetSection(Key).Value;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string sqlConnectionString = GetAppSettingsParameter("SqlConnectionString");
            services.AddHealthChecks();
            services.AddDbContext<ContactContext>(opt => opt.UseSqlServer(sqlConnectionString));
            services.AddMediatR(typeof(Mediator));
            services.AddCacheHelper();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IContactCommandRepository, ContactCommandRepository>();
            services.AddScoped<IContactQueryRepository, ContactQueryRepository>();
            services.AddScoped<IRequestHandler<DeleteContactCommand, EntityResponse<ContactResponse>>, DeleteContactHandler>();
            services.AddScoped<IRequestHandler<GetAllContactQuery, EntityResponse<IReadOnlyList<ContactResponse>>>, GetAllContactHandler>(); 
            services.AddScoped<IRequestHandler<CreateContactCommand, EntityResponse<CreateContactResponse>>, CreateContactHandler>();
            services.AddScoped<IRequestHandler<UpdateContactCommand, EntityResponse<UpdateContactResponse>>, UpdateContactHandler>();

            services.AddScoped<IContactCommandRepository, ContactCommandRepository>();
            services.AddControllers().AddNewtonsoftJson(json =>
            {
                json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                json.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                json.InputFormatterMemoryBufferThreshold = 31_457_280;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Contact.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contact.API v1"));
            }
            app.UseHealthChecks("/api/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
            {
                ResponseWriter = async (context, report) =>
                {
                    await context.Response.WriteAsync("I'm OK");
                }
            });


            app.UseMiddleware<RequestLoggerMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

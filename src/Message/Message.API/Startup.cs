using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Message.API.ViewModels;
using Message.Application.Interfaces;
using Message.Application.Services;
using Message.Core.Data;
using Message.Core.Providers;
using Message.Core.Repositories;
using Message.Infrastructure.Data;
using Message.Infrastructure.KeyGenerator;
using Message.Infrastructure.Providers;
using Message.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using static Message.API.ViewModels.MessageLineViewModel;

namespace Message.API
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
            #region Redis Dependencies

            //create ConnectionMultiplexer object given parameters
            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                var configuraitonParameters = ConfigurationOptions.Parse(Configuration.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuraitonParameters);
            });

            #endregion

            #region Project Dependencies

            services.AddScoped<IMessageDbContext, MessageDbContext>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IKeyGenerator, KeyGenarator>();
            services.AddScoped<IMessageQueueRepository, MessageQueueRepository>();
            services.AddScoped<IMessageHistoryRepository, MessageHistoryRepository>();
            services.AddScoped<IUserProvider, UserProvider>();
            services.AddTransient<IValidator<MessageLineViewModel>, MessageLineViewModelValidator>();

            services.AddAutoMapper(typeof(Startup));

            #endregion

            #region Swagger Dependencies

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Message API", Version = "v1" });
            });

            #endregion

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Message API V1");
            });
        }
    }
}

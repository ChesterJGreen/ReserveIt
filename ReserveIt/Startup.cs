using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ReserveIt.Data;
using ReserveIt.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ReserveIt
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
            services.AddControllers();
            services.AddRazorPages();
            services.AddScoped<IConferencesManager, ConferencesManager>();
            services.AddDbContext<ResContext>(options => options.UseSqlServer("name=ConnectionStrings:LocalDbConstr"));
            services.AddAutoMapper(config =>
            {
                Config.AutoMapperConfig.ConfigureAutoMapper(config);
            });
            services.AddSwaggerGen();
            services.AddAuthentication(Config.JwtTokenConfig.ConfigureJwtAuthentication)
                .AddJwtBearer(options => Config.JwtTokenConfig.ConfigureJwtBearerTokens(options, Configuration));
            services.AddScoped<IUserService, UserServiceAllowAll>();
            services.AddTransient<Config.BaseControllerDependencies>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                
            });
        }
        public void ConfigureAutoMapper()
        {
           
        }
    }
}

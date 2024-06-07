using BackEnd.BAL;
using BackEnd.DAL;
using BackEnd.DAL.JWTService;
using BackEnd.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
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
            services.AddDbContext<CIDbContext>(db => db.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddCors(cors => cors.AddPolicy("MyPolicy", builder =>
             {
                 builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
             }));

           services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "localhost",
                    ValidAudience = "localhost",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtConfig:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });
           
            services.AddScoped<BALLogin>();
            services.AddScoped<BALMission>();
            services.AddScoped<BALCMS>();
            services.AddScoped<BALCommon>();
            services.AddScoped<BALStory>();
            services.AddScoped<BALAdminUser>();
            services.AddScoped<BALVolunteeringTimesheet>();
            services.AddScoped<BALMissionSkill>();
            services.AddScoped<BALMissionTheme>();
            services.AddScoped<BALEmail>();
            services.AddScoped<DALLogin>();
            services.AddScoped<DALCMS>();           
            services.AddScoped<DALMission>();
            services.AddScoped<DALCommon>();
            services.AddScoped<DALStory>();
            services.AddScoped<DALAdminUser>();
            services.AddScoped<DALVolunteeringTimesheet>();
            services.AddScoped<DALMissionSkill>();
            services.AddScoped<DALMissionTheme>();
            services.AddScoped<DALEmail>();
            services.AddScoped<JwtService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BackEnd", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackEnd v1"));
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

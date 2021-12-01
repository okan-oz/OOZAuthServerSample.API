using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OOZAuthServereSample.Core.Configuration;
using OOZAuthServereSample.Core.Service;
using OOZAuthServerSample.SharedLibrary.Configurations;
using OOZAuthServerSample.Service.Services;
using OOZAuthServereSample.Core.Repositories;
using OOZAuthServerSample.Data.Repositories;
using OOZAuthServereSample.Core.Service.UnitOfWork;
using OOZAuthServerSample.Data;
using Microsoft.EntityFrameworkCore;
using OOZAuthServereSample.Core.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using OOZAuthServerSample.Service;

namespace OOZAuthServerSample.API
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
            //DI Register
            services.AddScoped<IAuthenticatonService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped(typeof(IServiceGeneric<,>), typeof(ServiceGeneric<,>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlServer"), sqlOption =>
                {
                    sqlOption.MigrationsAssembly("OOZAuthServerSample.Data");
                });
            });

            services.AddIdentity<UserApp, IdentityRole>(Opt =>
            {
                Opt.User.RequireUniqueEmail = true;
                Opt.Password.RequireNonAlphanumeric = false;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //Option
            services.Configure<CustomTokenOption>(Configuration.GetSection("TokenOption"));

            var tokenOptions = Configuration.GetSection("TokenOption").Get<CustomTokenOption>();

            services.Configure<List<Client>>(Configuration.GetSection("Clients"));

            services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,opts=> {

                    opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience=tokenOptions.Audince[0],
                        IssuerSigningKey=SignService.GetSymetricSecurityKey(tokenOptions.SecurityKey),
                        ValidateIssuerSigningKey=true,
                        ValidateAudience=true,
                        ValidateIssuer=true,
                        ValidateLifetime=true,
                        ClockSkew=System.TimeSpan.Zero
                        
                    };


                }) ;



            addSwagger(services);
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            useSwagger(app);
        }

        private void useSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{ Configuration["id"]}{ Configuration["version"]}");
            });
        }

        private void addSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = Configuration["id"], Version = Configuration["version"] });

            });

        }
    }
}

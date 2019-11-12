////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "Startup.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace FundooNoteApi
{
    using System;
    using BusinessLayer.Interface;
    using BusinessLayer.Service;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using RepositoryLayer.Context;
    using RepositoryLayer.Services;
    using RepositoryLayer.Interface;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;
    using Swashbuckle.AspNetCore.Swagger;
    using System.Collections.Generic;
    using Swashbuckle.AspNetCore.SwaggerGen;


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
            //Inject app settings
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
            services.AddTransient<IAccountManagerRepository, AccountManagerRepository>();
            services.AddTransient<IAccountManager, AccountManager>();
            services.AddTransient<INoteBusinessManager, NotesAccountManagerService>();
            services.AddTransient<INotesAccountManagerRepository, NotesAccountManagerRepository>();
            services.AddTransient<ILabelBusinessManager, LabelAccountManagerService>();
            services.AddTransient<ILabelAccountManager, LabelAccountManagerRepository>();
            services.AddTransient<IAdminBussiness, AdminService>();
            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<IAdminSpBusiness, AdminSpService>();
            services.AddTransient<IAdminSpRepository, AdminSpRepository>();


            //// database connection service is registered with method extension
            services.AddDbContext<AuthenticationContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<AuthenticationContext>();
           

            //// password validation
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            });


            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = false;
                    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                      {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                     };
                });

            ////In this code section we are reading App ID and App Secret from secrets.json file for the authentication purpose
            services.AddAuthentication().AddFacebook(facbookOptions =>
            {
                facbookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facbookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyFandooApp", Version = "v1", Description = "Fandoo App" });
                c.OperationFilter<FileUploadedOperation>();
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseHsts();
            }

            //// // global cors policy
            app.UseCors(builder =>
            builder.WithOrigins(Configuration["ApplicationSettings:Client_Url"].ToString()).AllowAnyHeader().AllowAnyMethod());
            app.UseHttpsRedirection();
           

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "MyFandooApp");
               
            });

            app.UseAuthentication();

            app.UseMvc();


        }
    }
    public class FileUploadedOperation : IOperationFilter
    {
        /// <summary>
        /// Apply function
        /// </summary>
        /// <param name="swaggerDocument">swaggerDocument parameter</param>
        /// <param name="documentFilter">documentFilter parameter </param>
        public void Apply(Operation swaggerDocument, OperationFilterContext documentFilter)
        {
            if (swaggerDocument.Parameters == null)
            {
                swaggerDocument.Parameters = new List<IParameter>();
            }

            swaggerDocument.Parameters.Add(new NonBodyParameter
            {
                Name = "Authorization",
                In = "header",
                Type = "string",
                Required = true
            });
        }
    }
}

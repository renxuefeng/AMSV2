using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using amsv2.Core.Configuration;
using amsv2.Repository;
using AMSV2.Helpers;
using AMSV2.Middleware;
using AMSV2.Models;
using AMSV2.Resources;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace AMSV2
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
            services.InitializationIOC(Configuration);
            services.AddDbContext<AMSV2DbContext>(options =>
   options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection")),ServiceLifetime.Transient);
            services.AddCors();
            // 自定义模型验证返回结果 by rxf 2019.08.26
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    ResponseData responseData = new ResponseData();
                    responseData.Success = false;
                    if (actionContext.ModelState.ErrorCount > 0)
                    {
                        responseData.Message = string.Join('|', actionContext.ModelState.Select(x => x.Value.Errors.FirstOrDefault().ErrorMessage).ToList().OrderByDescending(y => y));
                    }
                    return new JsonResult(responseData);
                };
            });
            services.AddSwaggerGen(c =>
            {
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "AMS API", Version = "v2" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference()
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    }, Array.Empty<string>() }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            var audienceConfiguration = Configuration.GetSection(nameof(AudienceConfiguration)).Get<AudienceConfiguration>();
            var keyByteArray = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audienceConfiguration.Secret));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.Audience = audienceConfiguration.Audience;
                    o.SaveToken = true;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidIssuer = audienceConfiguration.Issuer,
                        ValidateIssuer = true,
                        IssuerSigningKeys = new List<SecurityKey> { keyByteArray },
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                    };
                    o.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            // 如果过期，则把<是否过期>添加到，返回头信息中
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            // 配置调度任务
            //services.ConfigureQuartz();
            services.AddControllers().AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                    factory.Create(typeof(SharedResource));
            }); ;
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
                app.UseExceptionHandler(a => a.Run(async context =>
                {
                    var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = feature.Error;
                    ResponseData responseData = new ResponseData();
                    responseData.Success = false;
                    responseData.Message = exception.Message;
                    var result = JsonConvert.SerializeObject(responseData);
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(result);
                }));
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "AMS API V2");
                c.RoutePrefix = string.Empty;
            });
            app.UseAuthentication();
            // Use Localization
            app.ConfigureLocalization();
            //添加跨域
            app.UseCors(StartupHelper.MyAllowSpecificOrigins);
            // 记录请求中间件
            app.UseLogReqResponseMiddleware();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

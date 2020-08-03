﻿using amsv2.Core.Configuration;
using amsv2.Core.Dependency;
using amsv2.Repository;
using amsv2.Repository.IRepositories;
using amsv2.Repository.UnitOfWork;
using AMSV2.Authorization;
using AMSV2.Jobs;
using AMSV2.Models;
using AMSV2.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AMSV2.Helpers
{
    public static class StartupHelper
    {
        /// <summary>
        /// 初始化IOC
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection InitializationIOC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            var rootConfiguration = configuration.GetSection(nameof(RootConfiguration)).Get<RootConfiguration>();
            services.AddSingleton(rootConfiguration);
            services.AddSingleton<IAuthorizationPolicyProvider, WebMethodActionPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, WebMethodActionHandler>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ResponseData>();
            var audienceConfiguration = configuration.GetSection(nameof(AudienceConfiguration)).Get<AudienceConfiguration>();
            services.AddSingleton(audienceConfiguration);
            // 注册autoMapper服务
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // 多语言资源目录
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.ConfigureRepositories();
            services.ConfigureServices();
            return services;
        }
        /// <summary>
        /// 调度任务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureQuartz(this IServiceCollection services)
        {
            services.AddQuartz(q => {
                q.SchedulerId = "HandleLog";
                // we take this from appsettings.json, just show it's possible
                q.SchedulerName = "HandleLog Scheduler";

                // hooks LibLog to Microsoft logging without allowing it to detect concrete implementation
                // if you are using NLog, SeriLog or log4net you shouldn't need this
                //q.UseQuartzMicrosoftLoggingBridge();

                // we could leave DI configuration intact and then jobs need to have public no-arg constructor

                // the MS DI is expected to produce transient job instances 
                q.UseMicrosoftDependencyInjectionJobFactory(options =>
                {
                    // if we don't have the job in DI, allow fallback to configure via default constructor
                    options.AllowDefaultConstructor = true;
                });
                q.UseSimpleTypeLoader();
                // configure jobs with code
                var jobKey = new JobKey("HandleLog", "LogGroup");
                q.AddJob<HandleLogJob>(j => j
                    .StoreDurably()
                    .WithIdentity(jobKey)
                    .WithDescription("cleanup log job")
                );

                q.AddTrigger(t => t
                    .WithIdentity("HandleLog Trigger")
                    .ForJob(jobKey)
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromSeconds(10)).RepeatForever())
                    .WithDescription("cleanup log trigger")
                );
            });
            // ASP.NET Core hosting
            services.AddQuartzServer(options =>
            {
                // when shutting down we want jobs to complete gracefully
                options.WaitForJobsToComplete = true;
            });
            return services;
        }
        /// <summary>
        /// 注入仓储
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddSingleton(Assembly.Load("amsv2.Repository"), Assembly.Load("amsv2.Repository"));
            services.AddTransient(Assembly.Load("amsv2.Repository"), Assembly.Load("amsv2.Repository"));
            services.AddScoped(Assembly.Load("amsv2.Repository"), Assembly.Load("amsv2.Repository"));
            return services;
        }
        /// <summary>
        /// 注入服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton(Assembly.Load("amsv2.Service"), Assembly.Load("amsv2.Service"));
            services.AddTransient(Assembly.Load("amsv2.Service"), Assembly.Load("amsv2.Service"));
            services.AddScoped(Assembly.Load("amsv2.Service"), Assembly.Load("amsv2.Service"));
            return services;
        }
        /// <summary>
        /// 配置支持语言种类
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureLocalization(this IApplicationBuilder app)
        {
            //var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            //app.UseRequestLocalization(options.Value);
            var supportedCultures = new[] { "en", "zh" };
            app.UseRequestLocalization(options =>
                options
                    .AddSupportedCultures(supportedCultures)
                    .AddSupportedUICultures(supportedCultures)
                    .SetDefaultCulture(supportedCultures[1])
            );
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="app"></param>
        public static void SeedData(this IApplicationBuilder app)
        {
            
        }
    }
}

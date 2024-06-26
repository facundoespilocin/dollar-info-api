﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DollarInfo.DAL.Factory;
using DollarInfo.DAL.Repositories.Interfaces;
using DollarInfo.DAL.Repositories;
using DollarInfo.Services.Interfaces;
using DollarInfo.Utils.EmailService;
using DollarInfo.DAL.Aspect;
using DollarInfo.Services.Factories;

namespace DollarInfo.Services.Collection
{
    public static class ContainerServiceCollectionExtensions
    {
        public static void ServicesDependencyInjection(this IServiceCollection services, IConfiguration _configuration)
        {
            // Configuration
            services.AddSingleton(provider => (IConfigurationRoot)_configuration);

            // Factories
            services.AddTransient<IConnectionFactory, ConnectionFactory>();
            services.AddScoped<IndexUrlFactory>();

            // Services
            // Commented temp
            //services.AddSingleton(new TemplateService(
            //    Path.GetFullPath(
            //        Path.Combine(
            //            AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "DollarInfo.Utils", "EmailTemplates"))));
            services.AddSingleton(new TemplateService(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates"))));

            services.AddScoped<ICurrentUserAspect, CurrentUserAspect>();
            services.AddTransient<EmailService>();
            services.AddScoped<IRatesService, RatesService>();
            services.AddScoped<IBugReportService, BugReportService>();
            services.AddScoped<IIndexesService, IndexesService>();
            services.AddScoped<IProcessesService, ProcessesService>();

            // Repositories
            services.AddTransient<IMiscRepository, MiscRepository>();
            services.AddTransient<IProcessesRepository, ProcessesRepository>();
            services.AddTransient<IRatesRepository, RatesRepository>();
        }
    }
}
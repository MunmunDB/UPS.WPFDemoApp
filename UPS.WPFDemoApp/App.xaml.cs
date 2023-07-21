using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UPS.WPFDemoApp.Converters;
using UPS.WPFDemoApp.Helpers;
using UPS.WPFDemoApp.Helpers;
using UPS.WPFDemoApp.Helpers.Interfaces;
using UPS.WPFDemoApp.Models;
using UPS.WPFDemoApp.ViewModels;

namespace UPS.WPFDemoApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
            
            var mainWindow = ServiceProvider.GetRequiredService<EmployeeWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettingsSection>(Configuration.GetSection(nameof(AppSettingsSection)));
            services.AddScoped<IConfiguration>(_ => Configuration);
            services.AddTransient<IAPIUrlConfig, APIUrlConfig>();
            services.AddTransient<IEmployeeVM, EmployeeVM>();
            services.AddTransient<IEmployee, Employee>();
            services.AddTransient<IEmployeeDetailsVM, EmployeeDetailsVM>();
            services.AddTransient<IEmployeeToEmployeeDetails, EmployeeToEmployeeDetails>();
            services.AddTransient<IEmployeeHelper, EmployeeHelper>();
            
            services.AddTransient<IServiceClient, ServiceClient>();
            services.AddHttpClient();
            services.AddLogging();
            
            services.AddTransient(typeof(EmployeeWindow));
        }
    }
}

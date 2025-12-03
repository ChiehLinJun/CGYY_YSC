using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Quartz;
using System;
using System.Runtime.InteropServices;
using static CGYY_YSC.JOB.DoFinanceJob;

namespace CGYY_YSC
{
    internal class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        static void Main(string[] args)
        {
            ShowWindow(GetConsoleWindow(), 0);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration(app =>
        {
            app.AddJsonFile("appsettings.json");
        })
        .ConfigureServices((hostContext, services) =>
        {
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                var jobTypes = new[]
                {
                    typeof(DoFinance)
                };

                foreach (var jobType in jobTypes)
                {
                    AddJobAndTrigger(q, jobType, hostContext.Configuration);
                }
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        });

        public static void AddJobAndTrigger(
            IServiceCollectionQuartzConfigurator quartz,
            Type jobType,
            IConfiguration config)
        {
            string jobName = jobType.Name;

            var configKey = $"Quartz:{jobName}";
            var cronSchedule = config[configKey];

            if (string.IsNullOrEmpty(cronSchedule))
            {
                throw new Exception($"No Quartz.NET Cron schedule found for job in configuration at {configKey}");
            }

            var jobKey = new JobKey(jobName);
           
            quartz.AddJob(jobType, jobKey, (Action<IJobConfigurator>)null);
            quartz.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity(jobName + "-Trigger")
                .WithCronSchedule(cronSchedule));
        }

    }

}
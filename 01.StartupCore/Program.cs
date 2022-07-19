using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(builder =>
                {
                    Console.WriteLine("3��ִ�з�����Configure App Configuration");
                })
                .ConfigureHostConfiguration(builder =>
                {
                    Console.WriteLine("2��ִ�з�����Configure Host Configuration");
                })
                .ConfigureLogging(loggingBuilder =>
                {
                    Console.WriteLine("4.2��ִ�з�����ConfigureLogging");
                })
                .ConfigureServices(services =>
                {
                    Console.WriteLine("4.1��ִ�з�����Configure Services");
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    Console.WriteLine("1��ִ�з�����ConfigureWebHostDefaults");
                    webBuilder.UseStartup<Startup>();
                }

                );
    }
}

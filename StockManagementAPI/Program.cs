using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using StockManagementAPI.Seed;
using System;

namespace StockManagementAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Seeder.WriteSeed();
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

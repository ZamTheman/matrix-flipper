using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MatrixFlipperAPI.Services;
using MatrixFlipperAPI.DAL;
using Newtonsoft.Json;

namespace MatrixFlipperAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureServices((_, services) =>
                {
                    services.AddScoped<IMatrixService, MatrixService>();
                    services.AddScoped<IMatrixDataAccess, MatrixSQLDataAccess>();
                    services.AddControllers().AddNewtonsoftJson(options =>
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
                });
    }
}

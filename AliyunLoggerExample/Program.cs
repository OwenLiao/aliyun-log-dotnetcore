using Gemstar.Extensions.Logging.AliyunLogService;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AliyunLoggerExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext,builder) => {
                    builder.ClearProviders();
                    builder.AddConsole();
                    var aliyunLogOptions = new AliyunLogOptions();
                    hostingContext.Configuration.GetSection("AliyunLog").Bind(aliyunLogOptions);
                    builder.AddProvider(new AliyunLoggerProvider(aliyunLogOptions));
                })
                .UseStartup<Startup>();
    }
}

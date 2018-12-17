# aliyun-log-dotnetcore
.net core实现的阿里云日志组件，可以直接将应用程序中的日志写入到阿里云的日志服务中
# 使用方式
1. 安装nuget包
> dotnet add package Gemstar.Extensions.Logging.AliyunLogService
2. 配置aliyun log信息,修改appsettings.json，增加如下设置
```
  "AliyunLog": {
    "AccessKey": "YourAccessKey",
    "AccessSecret": "YourAccessSecret",
    "Endpoint": "YourEndPoint",
    "ProjectName": "YourProjectName",
    "LogStoreName": "YourLogStoreName",
    "SourceName": "YourSourceName",
    "LogLevel": "Information"//可选值：Trace（最详细，生产环境禁用），Debug（调试信息），Information（一般信息），Warning（警告），Error（错误），Critical（严重错误），None（不记录）
  }
```

3. 配置asp.net core日志组件为阿里云日志组件,修改program.cs
```
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
```


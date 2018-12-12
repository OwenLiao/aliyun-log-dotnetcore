using Aliyun.Api.LogService;
using Aliyun.Api.LogService.Domain.Log;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Gemstar.Extensions.Logging.AliyunLogService
{
    /// <summary>
    /// 阿里云日志记录器
    /// </summary>
    public class AliyunLogger : ILogger
    {
        private readonly Func<string, LogLevel, bool> _filter;
        private static ILogServiceClient _logClient;
        private readonly AliyunLogOptions _options;
        private readonly string _name;
        public AliyunLogger(string name,AliyunLogOptions options) : this(name, options, null)
        {

        }

        public AliyunLogger(string name,AliyunLogOptions options,Func<string, LogLevel, bool> filter)
        {
            if(options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _options = options;
            _name = name;
            _filter = filter;
            if(_logClient == null)
            {
                if(string.IsNullOrWhiteSpace(options.Endpoint) || string.IsNullOrWhiteSpace(options.ProjectName) || string.IsNullOrWhiteSpace(options.AccessKey) || string.IsNullOrWhiteSpace(options.AccessSecret))
                {
                    throw new ArgumentException("请先配置正确的阿里云日志相关参数值", nameof(options));
                }
                _logClient = LogServiceClientBuilders.HttpBuilder.Endpoint(options.Endpoint, options.ProjectName).Credential(options.AccessKey, options.AccessSecret).Build();
            }
        }


        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            if (!IsEnabled(logLevel)) return;

            var message = formatter(state, exception);
            var logInfo = new LogInfo
            {
                Contents =
                        {
                            {"level", logLevel.ToString()},
                            {"event", eventId.Name??""},
                            {"msg", message},
                        },
                Time = DateTimeOffset.Now
            };
            if (state is IEnumerable<KeyValuePair<string, object>> stateProperties)
            {
                foreach (var stateProperty in stateProperties)
                {
                    logInfo.Contents.Add(new KeyValuePair<string, string>(stateProperty.Key, stateProperty.Value?.ToString()));
                }
            }
            var logGroupInfo = new LogGroupInfo
            {
                Topic = _name??"",
                Logs = new List<LogInfo> { logInfo},
                Source = _options.SourceName??""
            };

            var response = _logClient.PostLogStoreLogsAsync(_options.LogStoreName, logGroupInfo);
            //response.Wait();
        }
        
        public bool IsEnabled(LogLevel logLevel)
        {
            if(logLevel < _options.LogLevel)
            {
                return false;
            }
            if(logLevel <= LogLevel.Information && _name.StartsWith("Microsoft.AspNetCore"))
            {
                //不记录asp.net core的一些信息
                return false;
            }
            return _filter == null || _filter(_name, logLevel); 
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gemstar.Extensions.Logging.AliyunLogService
{
    /// <summary>
    /// 阿里云日志记录器提供者
    /// </summary>
    public class AliyunLoggerProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, bool> _filter;
        private readonly AliyunLogOptions _options;

        public AliyunLoggerProvider(AliyunLogOptions options) : this(options, filter: null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionlessLoggerProvider"/> class.
        /// </summary>
        /// <param name="filter">The function used to filter events based on the log level.</param>
        public AliyunLoggerProvider(AliyunLogOptions options, Func<string, LogLevel, bool> filter)
        {
            _options = options;
            _filter = filter;
        }

        /// <inheritdoc />
        public ILogger CreateLogger(string name)
        {
            return new AliyunLogger(name, _options, _filter);
        }

        public void Dispose()
        {
        }
    }
}
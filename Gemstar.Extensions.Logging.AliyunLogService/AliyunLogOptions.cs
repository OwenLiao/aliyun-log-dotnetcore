using Microsoft.Extensions.Logging;

namespace Gemstar.Extensions.Logging.AliyunLogService
{
    /// <summary>
    /// 阿里云日志配置信息
    /// </summary>
    public class AliyunLogOptions
    {
        /// <summary>
        /// 阿里云api访问key
        /// </summary>
        public string AccessKey { get; set; }
        /// <summary>
        /// 阿里云api访问密钥
        /// </summary>
        public string AccessSecret { get; set; }
        /// <summary>
        /// 阿里云api访问地址
        /// </summary>
        public string Endpoint { get; set; }
        /// <summary>
        /// 阿里云日志项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 阿里云日志存储名称
        /// </summary>
        public string LogStoreName { get; set; }
        /// <summary>
        /// 日志记录级别
        /// </summary>
        public LogLevel LogLevel { get; set; }
        /// <summary>
        /// 来源名称
        /// </summary>
        public string SourceName { get; set; }
    }
}

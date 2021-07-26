using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using RestSharp;

namespace RestSharpClient
{
    /// <summary>
    /// 客户端工厂类
    /// </summary>
    public class RestSharpClientFactory
    {
        /// <summary>
        /// 寄存器
        /// </summary>
        private static readonly ConcurrentDictionary<string, RestClient> _clientAccess = new ConcurrentDictionary<string, RestClient>();

        /// <summary>
        /// 默认key
        /// </summary>
        private const string _defaultKey = "Default";

        static RestSharpClientFactory()
        {
            ServicePointManager.DefaultConnectionLimit = 256;
        }

        /// <summary>
        /// 创建实例
        /// 如果无则创建，有则从内存拿出
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="configureAction">初始化动作</param>
        /// <returns></returns>
        public static RestClient CreateClient(string key = _defaultKey, Action<RestSharpOption> configureAction = null)
        {
            return _clientAccess.GetOrAdd(key, s => ConfigureClient(configureAction));
        }

        private static RestClient ConfigureClient(Action<RestSharpOption> action)
        {
            var client = new RestClient();
            var option = new RestSharpOption();
            action?.Invoke(option);
            client.Timeout = option.TimeOut;
            if (!string.IsNullOrWhiteSpace(option.BaseUrl))
            {
                client.BaseUrl = new Uri(option.BaseUrl);
            }

            return client;
        }
    }

    /// <summary>
    /// 配置项
    /// </summary>
    public class RestSharpOption
    {
        /// <summary>
        /// url 可为ip+端口, 真正请求资源可在request设置
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// 超时时间 (s)
        /// </summary>
        public int TimeOut { get; set; } = 1000 * 30;
    }
}

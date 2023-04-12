using Furion;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using WeatherConsole.Extension;

namespace WeatherConsole
{
    public class StartUp : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {

            // 忽略 SSL 不安全检查，或 https 不安全或 https 证书有误
            services.AddHttpClient(string.Empty)
                   .ConfigurePrimaryHttpMessageHandler(u => new SocketsHttpHandler
                   {
                       SslOptions = new SslClientAuthenticationOptions
                       {
                           RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                       }
                   });

            //添加远程服务
            services.AddRemoteRequest(options =>
            {
                // 需在所有客户端注册之前注册
                options.ApproveAllCerts();
            });
        }
    }
}

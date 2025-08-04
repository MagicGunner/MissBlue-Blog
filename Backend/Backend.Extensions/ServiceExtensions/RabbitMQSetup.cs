using Backend.Common.Core;
using Backend.Common.EventBus.RabbitMQPersistent;
using Backend.Common.Option;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Backend.Extensions.ServiceExtensions {
    /// <summary>
    /// Db 启动服务
    /// </summary>
    public static class RabbitMQSetup {
        public static void AddRabbitMQSetup(this IServiceCollection services) {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var section = App.Configuration.GetSection("RabbitMQ");
            services.Configure<RabbitMQOptions>(section);

            var options = section.Get<RabbitMQOptions>();
            if (options is not { Enabled: true }) return;

            services.AddSingleton<IRabbitMQPersistentConnection>(sp => {
                                                                     var logger = sp.GetRequiredService<ILogger<RabbitMQPersistentConnection>>();
                                                                     var factory = new ConnectionFactory {
                                                                                                             HostName = options.Connection,
                                                                                                             DispatchConsumersAsync = true
                                                                                                         };

                                                                     if (!string.IsNullOrEmpty(options.UserName))
                                                                         factory.UserName = options.UserName;

                                                                     if (!string.IsNullOrEmpty(options.Password))
                                                                         factory.Password = options.Password;

                                                                     if (options.Port > 0)
                                                                         factory.Port = options.Port;

                                                                     return new RabbitMQPersistentConnection(factory, logger, options.RetryCount);
                                                                 });
            }
        }
    
}
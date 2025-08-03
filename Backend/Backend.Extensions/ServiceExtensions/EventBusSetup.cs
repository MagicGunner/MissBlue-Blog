using Autofac;
using Backend.Common;
using Backend.Common.EventBus.Eventbus;
using Backend.Common.EventBus.EventBusKafka;
using Backend.Common.EventBus.EventBusSubscriptions;
using Backend.Common.EventBus.RabbitMQPersistent;
using Backend.Extensions.EventHandling;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Backend.Extensions.ServiceExtensions {
    /// <summary>
    /// EventBus 事件总线服务
    /// </summary>
    public static class EventBusSetup {
        public static void AddEventBusSetup(this IServiceCollection services) {
            if (services == null) throw new ArgumentNullException(nameof(services));

            if (AppSettings.App(new string[] { "EventBus", "Enabled" }).ObjToBool()) {
                var subscriptionClientName = AppSettings.App("EventBus", "SubscriptionClientName");

                services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
                services.AddTransient<BlogQueryIntegrationEventHandler>();

                if (AppSettings.App(new string[] { "RabbitMQ", "Enabled" }).ObjToBool()) {
                    services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp => {
                                                                           var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                                                                           var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                                                                           var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                                                                           var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                                                                           var retryCount = 5;
                                                                           if (!string.IsNullOrEmpty(AppSettings.App("RabbitMQ", "RetryCount"))) {
                                                                               retryCount = int.Parse(AppSettings.App("RabbitMQ", "RetryCount"));
                                                                           }

                                                                           return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager,
                                                                                                       subscriptionClientName, retryCount);
                                                                       });
                }

                if (AppSettings.App("Kafka", "Enabled").ObjToBool()) {
                    services.AddHostedService<KafkaConsumerHostService>();
                    services.AddSingleton<IEventBus, EventBusKafka>();
                }
            }
        }
    }
}
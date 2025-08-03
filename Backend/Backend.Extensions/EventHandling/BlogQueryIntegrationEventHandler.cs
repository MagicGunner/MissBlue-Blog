using Backend.Common.EventBus.Eventbus;
using Backend.Common.Helper;
using Microsoft.Extensions.Logging;

namespace Backend.Extensions.EventHandling {
    public class BlogQueryIntegrationEventHandler : IIntegrationEventHandler<BlogQueryIntegrationEvent> {
        // private readonly IBlogArticleServices                      _blogArticleServices;
        // private readonly ILogger<BlogQueryIntegrationEventHandler> _logger;
        //
        // public BlogQueryIntegrationEventHandler(IBlogArticleServices                      blogArticleServices,
        //                                         ILogger<BlogQueryIntegrationEventHandler> logger) {
        //     _blogArticleServices = blogArticleServices;
        //     _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        // }
        //
        // public async Task Handle(BlogQueryIntegrationEvent @event) {
        //     _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, "Blog.Core", @event);
        //
        //     ConsoleHelper.WriteSuccessLine($"----- Handling integration event: {@event.Id} at Blog.Core - ({@event})");
        //
        //     await _blogArticleServices.QueryById(@event.BlogId.ToString());
        // }
        public Task Handle(BlogQueryIntegrationEvent @event) {
            throw new NotImplementedException();
        }
    }
}
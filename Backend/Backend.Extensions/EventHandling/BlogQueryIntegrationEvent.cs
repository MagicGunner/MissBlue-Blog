using Backend.Common.EventBus.Eventbus;

namespace Backend.Extensions.EventHandling {
    public class BlogQueryIntegrationEvent : IntegrationEvent {
        public string BlogId { get; private set; }

        public BlogQueryIntegrationEvent(string blogid) => BlogId = blogid;
    }
}
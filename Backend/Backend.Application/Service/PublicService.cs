using Backend.Common.Const;
using Backend.Common.EventBus.RabbitMQPersistent;
using Backend.Common.Option;
using Backend.Common.Redis;
using Backend.Contracts.IService;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Backend.Application.Service;

public class PublicService : IPublicService {
    private readonly IRedisBasketRepository        _redis;
    private readonly IRabbitMQPersistentConnection _rabbitMQ;
    private readonly ILogger<PublicService>        _logger;
    private readonly RabbitMQOptions               _options;

    public PublicService(IRedisBasketRepository redis, IRabbitMQPersistentConnection rabbitMq, ILogger<PublicService> logger) {
        _redis = redis;
        _rabbitMQ = rabbitMq;
        _logger = logger;
    }

    public async Task<string> RegisterEmailVerifyCode(string type, string email) {
        var key = $"{RedisConst.VERIFY_CODE}{type}{RedisConst.SEPARATOR}{email}";

        if (_redis.Exist(key).Result)
            return "请勿频繁获取验证码";

        var verifyCode = new Random().Next(100000, 999999).ToString();

        _redis.Set(key, verifyCode, TimeSpan.FromMinutes(RedisConst.VERIFY_CODE_EXPIRATION)).Wait();

        var msg = new Dictionary<string, object> {
                                                     ["email"] = email,
                                                     ["code"] = verifyCode,
                                                     ["type"] = type
                                                 };

        var json = JsonConvert.SerializeObject(msg);
        _rabbitMQ.PublishMessage(json, _options.EmailExchange!, _options.EmailRoutingKey!);

        _logger.LogInformation("验证码发送：邮箱={Email}，类型={Type}，验证码={Code}", email, type, verifyCode);

        return "验证码已发送，请注意查收！";
    }


    public void SendEmail(string type, string email, Dictionary<string, object>? content) {
        content ??= new Dictionary<string, object>();

        content["email"] = email;
        content["type"] = type;

        var json = JsonConvert.SerializeObject(content);
        _rabbitMQ.PublishMessage(json, _options.EmailExchange!, _options.EmailRoutingKey!);

        _logger.LogInformation("邮件通知消息发送完毕，时间={Time}，类型={Type}，收件人={Email}",
                               DateTime.Now, type, email);
    }
}
namespace Backend.Common.Option;

public class RabbitMQOptions {
    public bool   Enabled    { get; set; }
    public string Connection { get; set; }
    public string UserName   { get; set; }
    public string Password   { get; set; }
    public int    Port       { get; set; }
    public int    RetryCount { get; set; }

    public string EmailExchange   { get; set; }
    public string EmailRoutingKey { get; set; }

    public QueueNames      Queue      { get; set; } = new();
    public ExchangeNames   Exchange   { get; set; } = new();
    public RoutingKeys     RoutingKey { get; set; } = new();
    public ListenerOptions Listener   { get; set; } = new();
}

public class QueueNames {
    public string Email     { get; set; }
    public string LogLogin  { get; set; }
    public string LogSystem { get; set; }
}

public class ExchangeNames {
    public string Email { get; set; }
    public string Log   { get; set; }
}

public class RoutingKeys {
    public string Email     { get; set; }
    public string LogLogin  { get; set; }
    public string LogSystem { get; set; }
}

public class ListenerOptions {
    public SimpleListener Simple { get; set; } = new();
}

public class SimpleListener {
    public RetryOptions Retry { get; set; } = new();
}

public class RetryOptions {
    public bool Enabled         { get; set; }
    public int  MaxAttempts     { get; set; }
    public int  InitialInterval { get; set; }
    public int  Multiplier      { get; set; }
    public int  MaxInterval     { get; set; }
}
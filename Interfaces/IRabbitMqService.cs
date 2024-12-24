using RabbitMQ.Client;

public interface IRabbitMqService
{
    IConnection Connection { get; }

    void PublishCreateJson(object data);

    void PublishUpdateJson(object data);

    void PublishDeleteJson(object data);
}

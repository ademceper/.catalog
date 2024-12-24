using System;
using System.Text;
using System.Text.Json;  // System.Text.Json ile serileştirme
using RabbitMQ.Client;

public class RabbitMqService : IRabbitMqService, IDisposable
{
    private IConnection? _connection;
    private IModel? _channel;

    public IConnection Connection => _connection!;

    private const string CreateQueueName = "Create-Queue";
    private const string UpdateQueueName = "Update-Queue";
    private const string DeleteQueueName = "Delete-Queue";

    public RabbitMqService(string connectionString)
    {
        InitializeConnection(connectionString);
    }

    private void InitializeConnection(string connectionString)
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri(connectionString),
            ClientProvidedName = "Post App" 
        };

        _connection = factory.CreateConnection();

        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
            queue: CreateQueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        _channel.QueueDeclare(
            queue: UpdateQueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        _channel.QueueDeclare(
            queue: DeleteQueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );
    }

    public void PublishCreateJson(object data)
    {
        PublishJsonToQueue(CreateQueueName, data);
    }

    public void PublishUpdateJson(object data)
    {
        PublishJsonToQueue(UpdateQueueName, data);
    }

    public void PublishDeleteJson(object data)
    {
        PublishJsonToQueue(DeleteQueueName, data);
    }

    private void PublishJsonToQueue(string queueName, object data)
    {

        var jsonString = JsonSerializer.Serialize(data);

        var body = Encoding.UTF8.GetBytes(jsonString);

        _channel!.BasicPublish(
            exchange: "",
            routingKey: queueName,
            basicProperties: null,
            body: body
        );
    }

    public void Dispose()
    {
        _channel?.Close();
        _channel?.Dispose();

        _connection?.Close();
        _connection?.Dispose();
    }
}

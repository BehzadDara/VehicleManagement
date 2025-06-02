using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace VehicleManagement.Application.Publishers;

public class CarMessagePublisher
{
    private readonly IChannel _channel;

    private const string ExchangeName = "car_exchange";
    private const string QueueName = "car_queue";

    public CarMessagePublisher(IChannel channel)
    {
        _channel = channel;

        _channel.ExchangeDeclareAsync(
            exchange: ExchangeName, 
            type: ExchangeType.Fanout,
            durable: true,
            autoDelete: false
            );

        _channel.QueueDeclareAsync(
            queue: QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
            );

        _channel.QueueBindAsync(
            queue: QueueName, 
            exchange: ExchangeName, 
            routingKey: string.Empty
            );
    }

    public async Task PublishMessageAsync(CarMessage message, CancellationToken cancellationToken)
    {
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        await _channel.BasicPublishAsync(
            exchange: ExchangeName, 
            routingKey: string.Empty,
            body: body,
            cancellationToken: cancellationToken
            );
    }
}

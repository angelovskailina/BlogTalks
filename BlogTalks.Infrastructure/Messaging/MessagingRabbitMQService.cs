using BlogTalks.Application.Abstractions;
using BlogTalks.Application.Contracts;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace BlogTalks.Infrastructure.Messaging;

public class MessagingRabbitMQService(IOptions<RabbitMqSettings> rabbitmqSettings) : IMessagingService
{
    private readonly RabbitMqSettings _rabbitmqSettings = rabbitmqSettings.Value;

    public async Task Send(EmailDto email)
    {
        var factory = new ConnectionFactory
        {
            HostName = _rabbitmqSettings.RabbitURL,
            UserName = _rabbitmqSettings.Username,
            Password = _rabbitmqSettings.Password
        };

        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync(_rabbitmqSettings.ExchangeName, _rabbitmqSettings.ExchangeType,
            durable: true);
        await channel.QueueDeclareAsync(_rabbitmqSettings.QueueName, durable: true, exclusive: false,
            autoDelete: false);
        await channel.QueueBindAsync(_rabbitmqSettings.QueueName, _rabbitmqSettings.ExchangeName,
            _rabbitmqSettings.RouteKey);

        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(email));

        var prop = new BasicProperties
        {
            Persistent = true
        };

        await channel.BasicPublishAsync(
            exchange: _rabbitmqSettings.ExchangeName,
            routingKey: _rabbitmqSettings.RouteKey,
            mandatory: false,
            basicProperties: prop,
            body: body);
    }
}
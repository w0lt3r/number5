using System.Text.Json;
using System.Text.Json.Serialization;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.Extensions.Options;
using Number5Poc.Data.Entities;
using Number5Poc.Services.Interfaces;
using Number5Poc.Services.Options;

namespace Number5Poc.Services;

public class MessagingSystem: IMessagingSystem
{
    private readonly Kafka options;

    public MessagingSystem(
        IOptions<Kafka> options)
    {
        this.options = options.Value;
    }
    public async Task Publish(string operation)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = options.BootstrapServers
        };
        
        using (var client = new AdminClientBuilder(config).Build())
        {
            var topics = client.GetMetadata(TimeSpan.FromSeconds(10)).Topics;
            if (topics.Any(x => x.Topic == options.Topic) == false)
            {
                await client.CreateTopicsAsync(new TopicSpecification[]
                {
                    new TopicSpecification
                    {
                        Name = options.Topic
                    }
                });
            }
        }
        
        using (var producer = new ProducerBuilder<Null, string>(config).Build())
        {
            var result = await producer.ProduceAsync(options.Topic,
                new Message<Null, string> { Value= JsonSerializer.Serialize(new {
                    Id = Guid.NewGuid(),
                    Operation = operation
                }) });
        }
    }
}
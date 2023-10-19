using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.Options;
using Number5Poc.Data.Entities;
using Number5Poc.Services.Interfaces;
using Number5Poc.Services.Options;

namespace Number5Poc.Services;

public class AnalyticsHandler : IAnalyticsHandler
{
    private ElasticSearch options;
    public AnalyticsHandler(IOptions<ElasticSearch> options)
    {
        this.options = options.Value;
    }

    public async Task Push(Permission permission)
    {
        var client = new ElasticsearchClient(options.CloudId, new ApiKey(options.ApiKey));
        var existsResponse = await client.Indices.ExistsAsync(options.Index);
        if (!existsResponse.Exists)
        {
            await client.Indices.CreateAsync(options.Index);
        }
        var response = await client.IndexAsync(permission, options.Index);
    }
}
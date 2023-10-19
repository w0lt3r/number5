using Number5Poc.Data.Entities;

namespace Number5Poc.Services.Interfaces;

public interface IAnalyticsHandler
{
    Task Push(Permission permission);
}
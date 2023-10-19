namespace Number5Poc.Services.Interfaces;

public interface IMessagingSystem
{
    Task Publish(string operation);
}
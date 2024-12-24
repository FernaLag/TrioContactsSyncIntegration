namespace Trio.ContactSync.Domain.Contracts.Helpers
{
    public interface IApiClientFactory
    {
        HttpClient CreateHttpClient(string baseAddress);
    }
}
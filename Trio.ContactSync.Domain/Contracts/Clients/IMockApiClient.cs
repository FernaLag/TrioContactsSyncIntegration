namespace Trio.ContactSync.Domain.Contracts.Clients
{
    public interface IMockApiClient
    {
        Task<List<Contact>> GetAsync();
    }
}

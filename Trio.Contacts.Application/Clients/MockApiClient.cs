using Newtonsoft.Json;
using Trio.ContactSync.Domain;
using Trio.ContactSync.Application.Constants;
using Trio.ContactSync.Domain.Contracts.Clients;

namespace Trio.ContactSync.Application.Clients
{
    public class MockApiClient(HttpClient httpClient) : IMockApiClient
    {
        private readonly HttpClient _httpClient = httpClient;
        public async Task<List<Contact>> GetAsync()
        {
            string responseAsString = await _httpClient.GetStringAsync(MockApiConstants.GetContactsEndpoint);
            return JsonConvert.DeserializeObject<List<Contact>>(responseAsString);
        }
    }
}
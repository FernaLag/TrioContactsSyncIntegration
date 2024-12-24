using System.Net.Http.Headers;
using Trio.ContactSync.Domain.Contracts.Helpers;

namespace Trio.ContactSync.Application.Helpers
{
    public class ApiClientFactory : IApiClientFactory
    {
        public HttpClient CreateHttpClient(string baseAddress)
        {
            HttpClientHandler httpClientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            HttpClient httpClient = new(httpClientHandler)
            {
                BaseAddress = new Uri(baseAddress),
                Timeout = TimeSpan.FromMinutes(2)
            };

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }
    }
}
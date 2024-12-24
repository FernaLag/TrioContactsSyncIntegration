using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Trio.ContactSync.Application.Clients;
using Trio.ContactSync.Application.Constants;
using Trio.ContactSync.Application.Helpers;
using Trio.ContactSync.Domain;
using Trio.ContactSync.Domain.Contracts.Helpers;
using Xunit;

namespace Trio.ContactSync.Application.UnitTests.IntegrationTests
{
    public class MockApiIntegrationTests
    {
        private readonly MockApiClient _mockApiClient;

        public MockApiIntegrationTests()
        {
            ServiceCollection services = new();
            services.AddSingleton<IApiClientFactory, ApiClientFactory>();
            services.AddHttpClient();

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            IApiClientFactory apiClientFactory = serviceProvider.GetRequiredService<IApiClientFactory>();
            HttpClient httpClient = apiClientFactory.CreateHttpClient(MockApiConstants.BaseAddress);

            _mockApiClient = new MockApiClient(httpClient);
        }

        [Fact]
        public async Task GetContacts_Successfully()
        {
            // Act
            List<Contact> getResponse = await _mockApiClient.GetAsync();

            // Assert
            getResponse.ShouldNotBeEmpty();
        }
    }
}

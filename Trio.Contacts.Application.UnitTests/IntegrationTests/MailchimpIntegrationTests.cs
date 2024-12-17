using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Net.Http.Headers;
using Trio.ContactSync.Application.Clients;
using Trio.ContactSync.Application.Constants;
using Trio.ContactSync.Application.Helpers;
using Trio.ContactSync.Domain;
using Trio.ContactSync.Domain.Contracts.Helpers;
using Xunit;

namespace Trio.ContactSync.Application.UnitTests.IntegrationTests
{
    public class MailchimpIntegrationTests
    {
        private readonly MailchimpClient _mailchimpClient;

        public MailchimpIntegrationTests()
        {
            ServiceCollection services = new();
            services.AddSingleton<IApiClientFactory, ApiClientFactory>();
            services.AddHttpClient();

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            IApiClientFactory apiClientFactory = serviceProvider.GetRequiredService<IApiClientFactory>();
            HttpClient httpClient = apiClientFactory.CreateHttpClient(MailchimpConstants.BaseAddress);

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", MailchimpConstants.ApiKey);

            _mailchimpClient = new MailchimpClient(httpClient);
        }

        [Fact]
        public async Task PostAndDeleteMembersBatch_Successfully()
        {
            // Arrange
            List<MailchimpMember> mailchimpMembers =
            [
                new() { EmailAddress = "pablo@hotmail.com", Status = "subscribed" },
                new() { EmailAddress = "thaisa@gmail.com", Status = "subscribed" }
            ];

            // Act
            HttpResponseMessage postResponse = await _mailchimpClient.PostBatchAsync(mailchimpMembers);
            HttpResponseMessage deleteResponse = await _mailchimpClient.DeleteBatchAsync(mailchimpMembers);

            // Assert
            postResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
            deleteResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
        }
    }
}

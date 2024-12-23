﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Net.Http.Headers;
using Trio.ContactSync.Application.Clients;
using Trio.ContactSync.Application.Helpers;
using Trio.ContactSync.Domain;
using Trio.ContactSync.Domain.Contracts.Helpers;
using Xunit;

namespace Trio.ContactSync.Application.UnitTests.IntegrationTests
{
    public class MailchimpIntegrationTests
    {
        private readonly MailchimpClient _mailchimpClient;
        private readonly IConfiguration _configuration;

        public MailchimpIntegrationTests()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            ServiceCollection services = new();
            services.AddSingleton<IApiClientFactory, ApiClientFactory>();
            services.AddHttpClient();

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            IApiClientFactory apiClientFactory = serviceProvider.GetRequiredService<IApiClientFactory>();
            HttpClient httpClient = apiClientFactory.CreateHttpClient(_configuration["Mailchimp:BaseAddress"]);

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _configuration["Mailchimp:ApiKey"]);

            _mailchimpClient = new MailchimpClient(httpClient, _configuration);
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

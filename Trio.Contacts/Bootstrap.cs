using System.Net.Http.Headers;
using Trio.ContactSync.Application.Constants;
using Trio.ContactSync.Application.Clients;
using Trio.ContactSync.Application.Profiles;
using Trio.ContactSync.Application.Features.ContactSync.Handlers;
using Trio.ContactSync.Application.Helpers;
using Trio.ContactSync.Domain.Contracts.Clients;
using Trio.ContactSync.Domain.Contracts.Helpers;

namespace Trio.ContactSync.Api
{
    public class Bootstrap()
    {
        public void ConfigureServices(IServiceCollection services)
        {
            BindServices(services);
        }

        private static void BindServices(IServiceCollection services)
        {
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(SyncMockApiContactsToMailchimpRequestHandler).Assembly));
            services.AddAutoMapper(typeof(MailchimpProfile));
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            services.AddHttpClient();

            services.AddSingleton<IApiClientFactory, ApiClientFactory>();

            services.AddScoped<IMockApiClient>(serviceProvider =>
            {
                IApiClientFactory apiClientFactory = serviceProvider.GetRequiredService<IApiClientFactory>();
                HttpClient httpClient = apiClientFactory.CreateHttpClient(MockApiConstants.BaseAddress);

                return new MockApiClient(httpClient);
            });

            services.AddScoped<IMailchimpClient>(serviceProvider =>
            {
                IApiClientFactory apiClientFactory = serviceProvider.GetRequiredService<IApiClientFactory>();
                HttpClient httpClient = apiClientFactory.CreateHttpClient(MailchimpConstants.BaseAddress);

                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", MailchimpConstants.ApiKey);

                return new MailchimpClient(httpClient);
            });
        }
    }
}
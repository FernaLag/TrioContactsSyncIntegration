using Trio.ContactSync.Domain;
using MediatR;
using Trio.ContactSync.Application.Features.MockApi.Queries;
using Trio.ContactSync.Domain.Contracts.Clients;

namespace Trio.ContactSync.Application.Features.MockApi.Handlers
{
    public class GetMockApiContactsRequestHandler(IMockApiClient mockApiClient) : IRequestHandler<GetMockApiContactsRequest, List<Contact>>
    {
        private readonly IMockApiClient _mockApiClient = mockApiClient;

        public async Task<List<Contact>> Handle(GetMockApiContactsRequest request, CancellationToken cancellationToken)
        {
            return await _mockApiClient.GetAsync();
        }
    }
}
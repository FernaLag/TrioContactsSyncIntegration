using MediatR;
using Trio.ContactSync.Domain;

namespace Trio.ContactSync.Application.Features.MockApi.Queries
{
    public class GetMockApiContactsRequest : IRequest<List<Contact>>
    {
    }
}
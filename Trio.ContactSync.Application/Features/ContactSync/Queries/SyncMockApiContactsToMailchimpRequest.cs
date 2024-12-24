using MediatR;
using Trio.ContactSync.Domain;

namespace Trio.ContactSync.Application.Features.ContactSync.Queries
{
    public class SyncMockApiContactsToMailchimpRequest : IRequest<ContactSyncResult>
    {
    }
}
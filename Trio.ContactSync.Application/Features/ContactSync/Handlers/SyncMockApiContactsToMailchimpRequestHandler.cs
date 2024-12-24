using AutoMapper;
using MediatR;
using Trio.ContactSync.Application.Features.ContactSync.Queries;
using Trio.ContactSync.Application.Features.Mailchimp.Commands;
using Trio.ContactSync.Application.Features.MockApi.Queries;
using Trio.ContactSync.Domain;

namespace Trio.ContactSync.Application.Features.ContactSync.Handlers
{
    public class SyncMockApiContactsToMailchimpRequestHandler(IMediator mediator, IMapper mapper) : IRequestHandler<SyncMockApiContactsToMailchimpRequest, ContactSyncResult>
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        public async Task<ContactSyncResult> Handle(SyncMockApiContactsToMailchimpRequest request, CancellationToken cancellationToken)
        {
            List<Contact> mockApiContacts = await _mediator.Send(new GetMockApiContactsRequest(), cancellationToken);
            List<MailchimpMember> mailchimpMembers = _mapper.Map<List<MailchimpMember>>(mockApiContacts);

            await _mediator.Send(new UpdateMailchimpMembersCommand(mailchimpMembers), cancellationToken);

            return new ContactSyncResult(SyncedContacts: mockApiContacts.Count, Contacts: mockApiContacts);
        }
    }
}
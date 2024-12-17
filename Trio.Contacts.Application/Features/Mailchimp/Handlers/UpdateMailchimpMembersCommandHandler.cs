using MediatR;
using Trio.ContactSync.Domain.Contracts.Clients;

namespace Trio.ContactSync.Application.Features.Mailchimp.Handlers
{
    public class UpdateMailchimpMembersCommandHandler(IMailchimpClient mailchimpClient) : IRequestHandler<UpdateMailchimpMembersCommand>
    {
        private readonly IMailchimpClient _mailchimpClient = mailchimpClient;

        public async Task Handle(UpdateMailchimpMembersCommand request, CancellationToken cancellationToken)
        {
            await _mailchimpClient.PostBatchAsync(request.Members);
        }
    }
}
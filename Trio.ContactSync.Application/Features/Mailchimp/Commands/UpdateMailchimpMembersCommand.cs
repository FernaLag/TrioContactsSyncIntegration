using MediatR;
using Trio.ContactSync.Domain;

namespace Trio.ContactSync.Application.Features.Mailchimp.Commands
{
    public class UpdateMailchimpMembersCommand(List<MailchimpMember> mailchimpMembers) : IRequest
    {
        public List<MailchimpMember> Members { get; set; } = mailchimpMembers;
    }
}
using MediatR;
using Trio.ContactSync.Domain;

public class UpdateMailchimpMembersCommand(List<MailchimpMember> mailchimpMembers) : IRequest
{
    public List<MailchimpMember> Members { get; set; } = mailchimpMembers;
}

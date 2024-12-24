using Microsoft.Extensions.Configuration;

namespace Trio.ContactSync.Application.Configuration
{
    public class MailchimpSettings(IConfiguration configuration)
    {
        public string ListId { get; } = configuration["Mailchimp:ListId"];
        public string BaseAddress { get; } = configuration["Mailchimp:BaseAddress"];
        public string MembersListEndPoint { get; } = configuration["Mailchimp:MembersListEndPoint"];
    }
}

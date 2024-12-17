namespace Trio.ContactSync.Application.Constants
{
    public static class MailchimpConstants
    {
        public const string ListId = "41a403d5a9";
        public const string BaseAddress = "https://us7.api.mailchimp.com/3.0/";
        public const string BatchEndpoint = "batches";
        public const string MembersListEndPoint = $"/lists/{ListId}/members";

        public static class MemberStatus
        {
            public const string Subscribed = "subscribed";
            public const string Unsubscribed = "unsubscribed";
            public const string Cleaned = "cleaned";
            public const string Pending = "pending";
            public const string Transactional = "transactional";
        }
    }
}
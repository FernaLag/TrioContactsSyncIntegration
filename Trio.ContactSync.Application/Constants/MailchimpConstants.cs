namespace Trio.ContactSync.Application.Constants
{
    public static class MailchimpConstants
    {
        public const string BatchEndpoint = "batches";

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
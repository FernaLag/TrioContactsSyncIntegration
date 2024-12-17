namespace Trio.ContactSync.Domain.Contracts.Clients
{
    public interface IMailchimpClient
    {
        Task<HttpResponseMessage> PostBatchAsync(List<MailchimpMember> requestPayload);
    }
}

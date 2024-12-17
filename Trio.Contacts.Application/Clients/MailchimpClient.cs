using Newtonsoft.Json;
using Trio.ContactSync.Domain;
using Trio.ContactSync.Application.Constants;
using System.Text;
using Trio.ContactSync.Domain.Contracts.Clients;
using System.Security.Cryptography;

namespace Trio.ContactSync.Application.Clients
{
    public class MailchimpClient(HttpClient httpClient) : IMailchimpClient
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<HttpResponseMessage> PostBatchAsync(List<MailchimpMember> mailchimpMembers)
        {
            List<MailchimpOperation> operations = mailchimpMembers.Select((member, index) => new MailchimpOperation
            {
                Method = "POST",
                Path = MailchimpConstants.MembersListEndPoint,
                OperationId = $"operation-{index}",
                Body = JsonConvert.SerializeObject(new
                {
                    email_address = member.EmailAddress,
                    status = member.Status,
                    merge_fields = member.MergeFields
                })
            }).ToList();

            var batchRequest = new { operations };

            HttpResponseMessage response = await _httpClient.PostAsync(MailchimpConstants.BatchEndpoint, GetJsonContent(batchRequest));
            return response.EnsureSuccessStatusCode();
        }

        public async Task<HttpResponseMessage> DeleteBatchAsync(List<MailchimpMember> mailchimpMembers)
        {
            var operations = mailchimpMembers.Select(member => new
            {
                method = "DELETE",
                path = $"/lists/{MailchimpConstants.ListId}/members/{GetSubscriberHash(member.EmailAddress)}"
            }).ToList();

            var deleteBatchRequest = new { operations };

            HttpResponseMessage response = await _httpClient.PostAsync(MailchimpConstants.BatchEndpoint, GetJsonContent(deleteBatchRequest));
            return response.EnsureSuccessStatusCode();
        }

        private static string GetSubscriberHash(string email)
        {
            byte[] hash = MD5.HashData(Encoding.UTF8.GetBytes(email.ToLower()));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        private static StringContent GetJsonContent<T>(T request) => new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
    }
}
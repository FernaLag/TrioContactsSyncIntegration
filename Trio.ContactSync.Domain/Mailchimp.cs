using Newtonsoft.Json;

namespace Trio.ContactSync.Domain
{
    public class MailchimpMember
    {
        [JsonProperty("email_address")]
        public string EmailAddress { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("merge_fields")]
        public MergeFields MergeFields { get; set; }
    }

    public class MergeFields
    {
        [JsonProperty(nameof(FNAME))]
        public string FNAME { get; set; } = string.Empty;

        [JsonProperty(nameof(LNAME))]
        public string LNAME { get; set; } = string.Empty;
    }

    public class MailchimpOperation
    {
        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("operation_id")]
        public string OperationId { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }
    }
}
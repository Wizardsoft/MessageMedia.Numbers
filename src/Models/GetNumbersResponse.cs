using System;
using Newtonsoft.Json;

namespace MessageMedia.Numbers.Models
{
    public partial class GetNumbersResponse
    {
        [JsonProperty("data")]
        public GetNumbersResponseData[] Data { get; set; }

        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
    }

    public partial class GetNumbersResponseData
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("classification")]
        public string Classification { get; set; }

        [JsonProperty("available_after")]
        public DateTimeOffset AvailableAfter { get; set; }

        [JsonProperty("capabilities")]
        public GetNumbersCapanilites[] Capabilities { get; set; }
    }

    public enum GetNumbersCapanilites
    {
        SMS, TTS, MMS
    }

    public partial class Pagination
    {
        [JsonProperty("page_size")]
        public long PageSize { get; set; }

        [JsonProperty("next_token")]
        public Guid NextToken { get; set; }
    }
}

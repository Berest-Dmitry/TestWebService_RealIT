using System.Text.Json.Serialization;

namespace TestWebService_RealIT.Models.Response
{
    /// <summary>
    /// моедль результата запроса к API брусники со статусами
    /// </summary>
    public class BrusnikaResult
    {
        public string status { get; set; }

        [JsonPropertyName("x-request-id")]
        public Guid xRequestId { get; set; }

        public string codeError { get; set; }

        public string codeErrorExt { get; set; }

        public string message { get; set; }
    }
}

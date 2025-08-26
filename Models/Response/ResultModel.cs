using System.Text.Json.Serialization;

namespace TestWebService_RealIT.Models.Response
{
    /// <summary>
    /// модель результата, приходящего по HTTP
    /// </summary>
    public class ResultModel<T>
        where T : class
    {
        public BrusnikaResult result { get; set; }

        public T data { get; set; }

        public int totalNumberRecords { get; set; }
    }
}

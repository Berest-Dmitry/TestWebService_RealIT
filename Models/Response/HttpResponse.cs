namespace TestWebService_RealIT.Models.Response
{
    /// <summary>
    /// ответ, приходящий по запросу HTTP
    /// </summary>
    public class HttpResponse
    {
        public int StatusCode { get; set; }

        public string ResponseBody { get; set; }
    }
}

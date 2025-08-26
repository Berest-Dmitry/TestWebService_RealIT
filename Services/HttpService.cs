using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace TestWebService_RealIT.Services
{
    public class HttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private string _defaultURL = "http://contoso.com";

        public HttpService(HttpClient httpClient, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));  
            _logger = loggerFactory.CreateLogger<HttpService>();
        }

        public async Task<Models.Response.HttpResponse> SendRequest<T>(string path, HttpMethod httpMethod, T requestBody)
        {
            try
            {
                var jsonRequestContent = JsonSerializer.Serialize(requestBody);
                string baseURL = _configuration.GetSection("baseServiceURL").Value ?? _defaultURL;
                string url = Path.Combine(baseURL, path);
                var request = new HttpRequestMessage
                {
                    Method = httpMethod,
                    RequestUri = new Uri(url),
                    Content = new StringContent(jsonRequestContent, Encoding.UTF8,
                        MediaTypeNames.Application.Json)
                };
                string jwt = _configuration.GetSection("APIToken").Value ?? string.Empty;
                request.Headers.Authorization = new
                    System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt);

                var httpResponse = await _httpClient.SendAsync(request);
                var content = await httpResponse.Content.ReadAsStringAsync();

                var result = new Models.Response.HttpResponse
                {
                    StatusCode = (int)httpResponse.StatusCode,
                    ResponseBody = content
                };
                return result;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("An htttp-specific error occurred: \n" + ex.ToString());
                return null;
            }
            catch (Exception ex) { 
                _logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}

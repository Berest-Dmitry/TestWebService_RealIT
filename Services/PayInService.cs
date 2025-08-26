using System.Net;
using System.Text.Json;
using TestWebService_RealIT.Models.Response;

namespace TestWebService_RealIT.Services
{
    /// <summary>
    /// сервис работы с платежами 
    /// </summary>
    public class PayInService
    {
        private readonly MockService _mockService;
        private readonly HttpService _httpService;
        private readonly ILogger _logger;

        public PayInService(MockService mockService, HttpService httpService, ILoggerFactory loggerFactory)
        {       
            _mockService = mockService ?? throw new ArgumentNullException(nameof(mockService));
            _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
            _logger = loggerFactory.CreateLogger<PayInService>();
        }

        public async Task SendPayInRequest()
        {
            try
            {
                string methodPath = "host2host/payin";
                var payInRequest = _mockService.MockPayIn();
                var requestResult = await _httpService.SendRequest(methodPath, HttpMethod.Post, payInRequest);
                if (requestResult == null) {
                    return;
                }
                if (requestResult?.StatusCode != 200) {
                    throw new HttpRequestException("An error occurred during the request", null, (HttpStatusCode)requestResult.StatusCode);
                }
                var response = JsonSerializer.Deserialize<ResultModel<OperationData>>(requestResult?.ResponseBody);
                if(response?.data is null)
                {
                    throw new InvalidOperationException("Operation has not been created succsessfully, see details: " + response?.result.codeError);
                }
                var paymentDetails = response?.data?.paymentDetailsData;
                _logger.LogInformation($@"Payment details: 
                    nameMediator: {paymentDetails.nameMediator},
                    paymentMethod: {paymentDetails.paymentMethod},
                    bankName: {paymentDetails?.bankName},
                    number: {paymentDetails.number},
                    numberAdditional: {(!string.IsNullOrEmpty(paymentDetails.numberAdditional) ? paymentDetails.numberAdditional : "none")},
                    qRcode: {(!string.IsNullOrEmpty(paymentDetails.qRCode) ? paymentDetails.qRCode : "none")}");
            }
            catch (Exception ex) {
                _logger.LogError(ex.ToString());
            }
        }
    }
}

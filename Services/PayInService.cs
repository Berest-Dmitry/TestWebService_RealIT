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
                    nameMediator: {MaskString(paymentDetails.nameMediator, 4)},
                    paymentMethod: {paymentDetails.paymentMethod},
                    bankName: {paymentDetails?.bankName},
                    number: {MaskString(paymentDetails?.number, 12)},
                    numberAdditional: {MaskString(paymentDetails?.numberAdditional, 6)},
                    qRcode: {MaskString(paymentDetails?.qRCode, 6)}");
            }
            catch (Exception ex) {
                _logger.LogError(ex.ToString());
            }
        }

        /// <summary>
        /// маскировка данных строки
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string MaskString(string input, int charactersToMask) { 
            if(string.IsNullOrEmpty(input)) return "not set";

            string maskedString = "";
            if (charactersToMask >= input.Length)
            {
                maskedString = new string('*', input.Length);            
            }
            else
            {
                string maskedPart = new string('*', charactersToMask);
                string remainingPart = input.Substring(charactersToMask);
                maskedString = maskedPart + remainingPart;               
            }
            return maskedString;
        }

    }
}

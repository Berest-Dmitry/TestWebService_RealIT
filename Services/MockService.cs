using TestWebService_RealIT.Common;
using TestWebService_RealIT.Models.Request;

namespace TestWebService_RealIT.Services
{
    /// <summary>
    /// сервис генерации тестовых данных для отправки запросов
    /// </summary>
    public class MockService
    {
        private readonly string _mockIPAddress;
        public MockService(IConfiguration configuration) {
            _mockIPAddress = configuration.GetValue<string>("mockIP") 
                ?? throw new ArgumentNullException("mockIP");
        }
        public PayInRequest MockPayIn()
        {
            var random = new Random();
            return new PayInRequest
            {
                clientID = Guid.NewGuid(),
                clientIP = _mockIPAddress,
                clientDateCreated = new DateTime(2000, 1, 1).ToString("yyyy-MM-ddThh:mm:ss.fffZ"),
                paymentMethod = DefaultEnums.GetEnumDescription(Common.DefaultEnums.PaymentMethod.toCard),
                idTransactionMerchant = Guid.NewGuid(),
                amount = random.Next(100, 199)

            };
        }
    }
}

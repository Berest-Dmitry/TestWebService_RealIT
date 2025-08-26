using static TestWebService_RealIT.Common.DefaultEnums;

namespace TestWebService_RealIT.Models.Request
{
    /// <summary>
    /// запрос на платеж от клиента
    /// </summary>
    public class PayInRequest
    {
        public Guid clientID { get; set; }

        public string clientIP { get; set; }

        public string clientDateCreated { get; set; }

        public string paymentMethod { get; set; }

        public Guid idTransactionMerchant {  get; set; }

        public float amount { get; set; }
    }
}

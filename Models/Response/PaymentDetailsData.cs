namespace TestWebService_RealIT.Models.Response
{
    /// <summary>
    /// модель данных о реквизитах платежа
    /// </summary>
    public class PaymentDetailsData
    {
        public string nameMediator { get; set; }

        public string paymentMethod { get; set; }

        public string bankName { get; set; }

        public string number {  get; set; }

        public string? numberAdditional { get; set; }

        public string qRCode { get; set; }
    }
}

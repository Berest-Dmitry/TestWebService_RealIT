namespace TestWebService_RealIT.Models.Response
{
    /// <summary>
    /// модель данных о выполненной операции
    /// </summary>
    public class OperationData
    {
        public Guid id { get; set; }

        public DateTime dateAdded { get; set; }

        public DateTime? dateUpdated { get; set; }

        public string typeOperation {  get; set; }

        public string status { get; set; }

        public float amountInitial { get; set; }

        public float amountRandomized { get; set; }

        public float amount { get; set; } 
        public float amountComission { get; set; } 

        public float amountInCurrencyBalance {  get; set; }

        public float amountComissionInCurrencyBalance { get; set; }

        public string currency {  get; set; }

        public Guid idTransactionMerchant { get; set; }

        public PaymentDetailsData paymentDetailsData { get; set; }

    }
}

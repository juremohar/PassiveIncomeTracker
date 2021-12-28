namespace PassiveIncomeTracker.Models
{
    public class InsertInterestModel 
    {
        public int IdCryptoCurrency { get; set; }
        public int IdInterestPayout { get; set; }
        public double Amount { get; set; }
        public double Interest { get; set; }
        public string? Note { get; set; } 
    }

    public class UpdateInterestModel
    {
        public double Amount { get; set; }
        public double Interest { get; set; }
        public string? Note { get; set; }
    }

    public class InterestModel
    {
        public int IdUserInterest {  get; set; }
        public int IdUser { get; set; }
        public CryptocurrencyModel Cryptocurrency { get; set; }
        public double Amount { get; set; }
        public double Interest { get; set; }
        public string Note { get; set; }
        public DateTime InsertedAt { get; set; }
    }

    public class UserCrypoBalanceModel 
    {
        public int IdCryptocurrency { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double CompoundedAmount { get; set; }
        public double AverageInterest { get; set; }
    }

    public class GetUserCryptoBalanceFilterModel 
    {
        public int IdUser {  get; set; }
        public int? IdCryptocurrency { get; set; }
        // inserted at
        // query
        // time frame
    }
}

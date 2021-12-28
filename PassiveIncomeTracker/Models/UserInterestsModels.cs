namespace PassiveIncomeTracker.Models
{
    public class InsertUserInterestModel 
    {
        public int IdCryptoCurrency { get; set; }
        public int IdInterestPayout { get; set; }
        public double Amount { get; set; }
        public double InterestRate { get; set; }
        public string? Note { get; set; } 
    }

    public class UpdateUserInterestModel
    {
        public double Amount { get; set; }
        public double InterestRate { get; set; }
        public string? Note { get; set; }
    }

    public class UserCrypoBalanceModel 
    {
        public int IdCryptocurrency { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double CompoundedAmount { get; set; }
        public double AverageInterestRate { get; set; }
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

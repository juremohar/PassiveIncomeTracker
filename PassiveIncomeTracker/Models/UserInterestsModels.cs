namespace PassiveIncomeTracker.Models
{
    public class InsertUserInterestModel 
    {
        public int IdCryptoCurrency { get; set; }
        public int IdInterestPayout { get; set; }
        public int IdService { get; set; }
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

    public class UserCrypoInterestInformationModel 
    {
        public int IdCryptocurrency { get; set; }
        public int CoinMarketCapId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double CompoundedAmount { get; set; }
        public double AverageInterestRate { get; set; }
        public DifferentIntervalsInterestModel DifferrentIntervalsInterest { get; set; }
    }

    public class DifferentIntervalsInterestModel 
    {
        public double Daily { get; set; }
        public double Weekly { get; set; }
        public double Monthly { get; set; }
        public double Yearly { get; set; }
    }

    public class UserCryptosInterestsInformationModel 
    {
        public List<UserCrypoInterestInformationModel> CryptosInterests { get; set; }
        public DifferentIntervalsInterestsEarningsModel DifferentIntervalsInterestsEarnings { get; set; }
    }

    public class DifferentIntervalsInterestsEarningsModel 
    {
        public double Daily { get; set; }
        public double Weekly { get; set; }
        public double Monthly { get; set; }
        public double Yearly { get; set; }
    }

    public class UserCryptoInputsModel 
    {
        public int IdUserInterest { get; set; }
        public int IdCryptocurrency { get; set; }
        public InterestPayoutModel InterestPayout { get; set; }
        public ServiceModel Service { get; set; }
        public double Amount { get; set; }
        public double Rate { get; set; }

        public string? Note { get; set; }
        public DateTime InsertedAt { get; set; } 
    }
}

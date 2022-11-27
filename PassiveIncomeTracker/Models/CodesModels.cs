namespace PassiveIncomeTracker.Models
{
    public class InterestPayoutModel
    {
        public int IdInterestPayout { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
    }

    public class ServiceModel
    {
        public int IdService { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
    }

    public class CountryModel
    {
        public int IdCountry { get; set; }
        public string Short { get; set; }
        public string Name { get; set; }
    }
}

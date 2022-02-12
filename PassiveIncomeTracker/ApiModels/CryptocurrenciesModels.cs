namespace PassiveIncomeTracker.ApiModels
{
    public class InsertCryptocurrencyModel
    {
        public string Code { get; set; }
        public string Name { get; set; }   
        public double Price { get; set; }
        public int CoinMarketCapId { get; set; }
    }

    public class GetCryptocurrenciesFilterModel 
    {
        public int Page { get; set; } = 1;
        public int PerPage { get; set; } = 100;
        public string? Query { get; set; }

        // TODO: filter by
    }
}

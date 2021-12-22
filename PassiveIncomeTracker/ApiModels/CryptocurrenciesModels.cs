namespace PassiveIncomeTracker.ApiModels
{
    public class InsertCryptocurrencyModel
    {
        public string Code { get; set; }
        public string Name { get; set; }   
        public double Price { get; set; }
        public int CoinMarketCapId { get; set; }
    }
}

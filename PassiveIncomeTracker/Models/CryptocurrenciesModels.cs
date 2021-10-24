namespace PassiveIncomeTracker.Models
{
    public class CryptocurrencyModel
    {
        public int IdCryptocurrency { get; set; }
        public string Code { get;set; }
        public string Name { get;set; }
        public double Price { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}

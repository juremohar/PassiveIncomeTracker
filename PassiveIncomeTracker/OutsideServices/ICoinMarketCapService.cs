namespace PassiveIncomeTracker.OutsideServices
{
    public interface ICoinMarketCapService
    {
        Task<List<CmcTokenModel>> GetLatest();
    }

    // TODO: this is ugly, need to find a better way to organize this
    // I will leave it as POC

    public class CmcTokenModel 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double Price { get; set; }
    }

    public class CmcLatestListingsResponseModel 
    {
        public List<Token> Data { get; set; }
    }

    public class Token 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public Quote Quote { get; set; }
    }

    public class Quote 
    {
        public USD USD { get; set; }
    } 

    public class USD 
    {
        public double Price { get; set; }
    }
}

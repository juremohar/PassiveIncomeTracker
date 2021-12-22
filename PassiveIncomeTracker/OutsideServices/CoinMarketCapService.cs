using RestSharp;

namespace PassiveIncomeTracker.OutsideServices
{
    public class CoinMarketCapService : ICoinMarketCapService
    {
        private readonly IConfiguration _configuration;

        private readonly RestClient _client = null;

        public CoinMarketCapService
        (
            IConfiguration configuration
        )
        {
            _configuration = configuration;

            _client = new RestClient("https://pro-api.coinmarketcap.com/v1");
            //_client = new RestClient("https://sandbox-api.coinmarketcap.com/v1");
            _client.AddDefaultHeader("Accept", "application/json");
            _client.AddDefaultHeader("X-CMC_PRO_API_KEY", _configuration.GetSection("CoinMarketCapKey").Value);
            //_client.AddDefaultHeader("X-CMC_PRO_API_KEY", "b54bcf4d-1bca-4e8e-9a24-22ff2c3d462c");
        }

        public async Task<List<CmcTokenModel>> GetLatest()
        {
            var request = new RestRequest("cryptocurrency/listings/latest", DataFormat.Json)
                 .AddParameter("limit", 10);

            var data = await _client.GetAsync<CmcLatestListingsResponseModel>(request, CancellationToken.None);

            var list = new List<CmcTokenModel>();

            foreach (var token in data.Data) 
            {
                list.Add(new CmcTokenModel
                {
                    Id = token.Id,
                    Name = token.Name,
                    Symbol = token.Symbol,
                    Price = token.Quote.USD.Price
                });
            }

            return list;
        }
    }
}

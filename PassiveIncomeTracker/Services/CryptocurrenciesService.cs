using PassiveIncomeTracker.ApiModels;
using PassiveIncomeTracker.DbModels;
using PassiveIncomeTracker.Interfaces;
using PassiveIncomeTracker.OutsideServices;

namespace PassiveIncomeTracker.Services
{
    public class CryptocurrenciesService : ICryptocurrenciesService
    {
        private readonly DbApi _db;
        private readonly ICoinMarketCapService _coinMarketCapService;


        public CryptocurrenciesService
        (
            DbApi db,
            ICoinMarketCapService coinMarketCapService
        )
        {
            _db = db;
            _coinMarketCapService = coinMarketCapService;
        }

        public void Insert(InsertCryptocurrencyModel model)
        {
            var row = new TCryptocurrency
            {
                Code = model.Code,
                Name = model.Name,
                Price = model.Price,
                CoinMarketCapId = model.CoinMarketCapId
            };

            _db.Cryptocurrencies.Add(row);
            _db.SaveChanges();
        }

        public async Task UpdateCryptoWithLatestData() 
        {
            var latestCryptoData = await _coinMarketCapService.GetLatest();

            var crypocurrencies = new List<TCryptocurrency>();

            foreach (var item in latestCryptoData) 
            {
                crypocurrencies.Add(new() 
                {   
                    CoinMarketCapId = item.Id,
                    Code = item.Symbol,
                    Name = item.Name,
                    Price = item.Price,
                    InsertedAt = DateTime.UtcNow
                });
            }

            // TODO: optimize this to bulk insert, for now it is okay ...
            var cryptoInDb = _db
                .Cryptocurrencies
                .ToList();

            var cryptoIdsNotInDb = crypocurrencies.Select(x => x.CoinMarketCapId).ToList().Except(cryptoInDb.Select(x => x.CoinMarketCapId));

            var cryptoNotInDb = crypocurrencies.Where(x => cryptoIdsNotInDb.Contains(x.CoinMarketCapId)).ToList();

            await _db.AddRangeAsync(cryptoNotInDb);

            foreach (var crypto in cryptoInDb) 
            {
                var updatedCrypto = crypocurrencies.FirstOrDefault(x => x.CoinMarketCapId == crypto.CoinMarketCapId);

                if (updatedCrypto != null) 
                {
                    crypto.Price = updatedCrypto.Price;
                }
            }

            await _db.SaveChangesAsync();
        }
    }
}

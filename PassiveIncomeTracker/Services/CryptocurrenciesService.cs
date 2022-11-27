using PassiveIncomeTracker.ApiModels;
using PassiveIncomeTracker.DbModels;
using PassiveIncomeTracker.Interfaces;
using PassiveIncomeTracker.Models;
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

        public List<CryptocurrencyModel> Get(GetCryptocurrenciesFilterModel model)
        {
            var cryptocurrencies = _db.Cryptocurrencies.AsEnumerable();

            if (model.Query != null) 
            {
                cryptocurrencies = cryptocurrencies
                    .Where(x => 
                        x.Name.Contains(model.Query) ||
                        x.Code.Contains(model.Query)
                    );
            }

            // TODO: we dont have marketcap atm so we order by idCrypto, but implement marketcap
            cryptocurrencies = cryptocurrencies
                .OrderBy(x => x.IdCryptocurrency)
                .Skip((model.Page - 1) * model.PerPage)
                .Take(model.PerPage);

            var result = cryptocurrencies.Select(x => new CryptocurrencyModel
            {
                IdCryptocurrency = x.IdCryptocurrency,
                Code = x.Code,
                Name = x.Name,
                Price = x.Price,
                CoinMarketCapId = x.CoinMarketCapId
            })
            .ToList();

            return result;
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

        public CryptocurrencyModel GetById(int idCryptocurrency)
        {
            var cryptocurrency = _db
                .Cryptocurrencies
                .SingleOrDefault(x => x.IdCryptocurrency == idCryptocurrency);

            if (cryptocurrency == null)
            {
                throw new UserException("Invalid idCryptocurrency param");
            }

            return new CryptocurrencyModel
            {
                IdCryptocurrency = cryptocurrency.IdCryptocurrency,
                Code = cryptocurrency.Code,
                Name = cryptocurrency.Name,
                Price = cryptocurrency.Price,
                CoinMarketCapId = cryptocurrency.CoinMarketCapId
            };
        }
    }
}

using PassiveIncomeTracker.ApiModels;
using PassiveIncomeTracker.DbModels;
using PassiveIncomeTracker.Interfaces;

namespace PassiveIncomeTracker.Services
{
    public class CryptocurrenciesService : ICryptocurrenciesService
    {
        private readonly DbApi _db;

        public CryptocurrenciesService
        (
            DbApi db
        )
        { 
            _db = db;
        }

        public void Insert(InsertCryptocurrencyModel model)
        {
            var row = new TCryptocurrency
            {
                Code = model.Code,
                Name = model.Name,
                Price = model.Price,
                ThumbnailUrl = model.ThumbnailUrl
            };

            _db.Cryptocurrencies.Add(row);
            _db.SaveChanges();
        }
    }
}

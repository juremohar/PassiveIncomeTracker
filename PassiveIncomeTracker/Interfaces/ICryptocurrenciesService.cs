using PassiveIncomeTracker.ApiModels;
using PassiveIncomeTracker.Models;

namespace PassiveIncomeTracker.Interfaces
{
    public interface ICryptocurrenciesService
    {
        void Insert(InsertCryptocurrencyModel model);
        Task UpdateCryptoWithLatestData();
        List<CryptocurrencyModel> Get(GetCryptocurrenciesFilterModel model);
    }
}

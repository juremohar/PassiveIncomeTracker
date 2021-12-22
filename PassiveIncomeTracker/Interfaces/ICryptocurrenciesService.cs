using PassiveIncomeTracker.ApiModels;
using PassiveIncomeTracker.DbModels;

namespace PassiveIncomeTracker.Interfaces
{
    public interface ICryptocurrenciesService
    {
        void Insert(InsertCryptocurrencyModel model);
        Task UpdateCryptoWithLatestData();
    }
}

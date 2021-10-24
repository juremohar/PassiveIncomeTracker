using PassiveIncomeTracker.ApiModels;

namespace PassiveIncomeTracker.Interfaces
{
    public interface ICryptocurrenciesService
    {
        public void Insert(InsertCryptocurrencyModel model);
    }
}

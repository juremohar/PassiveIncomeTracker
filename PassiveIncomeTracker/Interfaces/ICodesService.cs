using PassiveIncomeTracker.Models;

namespace PassiveIncomeTracker.Interfaces
{
    public interface ICodesService
    {
        List<ServiceModel> GetServices();
        List<InterestPayoutModel> GetInterestsPayoutes();
        List<CountryModel> GetCountries();
    }
}

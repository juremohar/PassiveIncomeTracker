using PassiveIncomeTracker.ApiModels;
using PassiveIncomeTracker.Models;

namespace PassiveIncomeTracker.Interfaces
{
    public interface IUserInterestService
    {
        Task InsertUserInterest(InsertUserInterestModel model);
        Task UpdateUserInterest(int id, UpdateUserInterestModel model);
        Task<List<UserCrypoBalanceModel>> GetUserCryptoBalance(GetUserCryptoBalanceFilterModel model); // TODO: this needs to be more generic  
        Task CalculateUsersInterests();
    }
}

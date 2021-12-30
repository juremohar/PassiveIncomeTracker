using PassiveIncomeTracker.ApiModels;
using PassiveIncomeTracker.Models;

namespace PassiveIncomeTracker.Interfaces
{
    public interface IUserInterestService
    {
        Task InsertUserInterest(InsertUserInterestModel model);
        Task UpdateUserInterest(int id, UpdateUserInterestModel model);
        Task<UserCryptosInterestsInformationModel> GetUserCryptosInterestsInformation(int idUser);
        Task CalculateUsersInterests();
    }
}

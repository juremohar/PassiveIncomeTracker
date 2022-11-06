using PassiveIncomeTracker.Models;

namespace PassiveIncomeTracker.Interfaces
{
    public interface IUserInterestService
    {
        Task InsertUserInterest(InsertUserInterestModel model);
        Task UpdateUserInterest(int id, UpdateUserInterestModel model);
        Task DeleteUserInterest(int id);
        Task<UserCryptoInputsModel> GetUserInterestById(int id);
        Task<UserCryptosInterestsInformationModel> GetUserCryptosInterestsInformation(int idUser);
        Task CalculateUsersInterests();
        Task<List<UserCryptoInputsModel>> GetUserCryptocurrencyInputs(int idUser, int idCryptocurrency);
    }
}

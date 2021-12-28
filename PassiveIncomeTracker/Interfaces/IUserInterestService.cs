﻿using PassiveIncomeTracker.ApiModels;
using PassiveIncomeTracker.Models;

namespace PassiveIncomeTracker.Interfaces
{
    public interface IUserInterestService
    {
        void InsertInterest(InsertInterestModel model);
        void UpdateInterest(int id, UpdateInterestModel model);
        void DeleteInterest(int id);
        Task<List<UserCrypoBalanceModel>> GetUserCryptoBalance(GetUserCryptoBalanceFilterModel model); // TODO: this needs to be more generic  
        InterestModel GetInterestById(int id);
        Task CalculateUsersInterests();
    }
}

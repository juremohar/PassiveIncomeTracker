﻿using Microsoft.EntityFrameworkCore;
using PassiveIncomeTracker.DbModels;
using PassiveIncomeTracker.Interfaces;
using PassiveIncomeTracker.Models;
using PassiveIncomeTracker.Helpers;

namespace PassiveIncomeTracker.Services
{
    public class UserInterestService : IUserInterestService
    {
        private readonly DbApi _db;
        private readonly IAuthService _authService;

        public UserInterestService
        (
            DbApi db,
            IAuthService authService
        ) 
        {
            _db = db;
            _authService = authService;
        }

        public InterestModel GetInterestById(int id)
        {
            throw new NotImplementedException();
        }

        public List<InterestModel> GetInterests(GetInterestFilterModel model)
        {
            throw new NotImplementedException();
        }

        public void InsertInterest(InsertInterestModel model)
        {
            var cryptocurrency = _db.Cryptocurrencies.FirstOrDefault(x => x.IdCryptocurrency == model.IdCryptoCurrency);
            if (cryptocurrency == null) 
            {
                throw new Exception("Invalid crypocurrency param");
            }

            var interestPayout = _db.InterestPayouts.FirstOrDefault(x => x.IdInterestPayout == model.IdInterestPayout);
            if (interestPayout == null) 
            {
                throw new Exception("Invalid interest payout param");
            }

            if (model.Amount <= 0) 
            {
                throw new Exception("Invalid amount");
            }

            if (model.Interest <= 0) 
            {
                throw new Exception("Invalid interest");
            }

            var interest = new TUserInterest
            {
                IdCryptocurrency = cryptocurrency.IdCryptocurrency,
                IdUser = _authService.GetLoggedInUser().IdUser,
                IdInterestPayout = model.IdInterestPayout,
                OriginalAmount = model.Amount,
                CompoundedAmount = model.Amount,
                Interest = model.Interest,
                Note = model.Note
            };

            _db.UsersInterests.Add(interest);
            _db.SaveChanges();
        }

        public void UpdateInterest(int id, UpdateInterestModel model)
        {
            var userInterest = _db.UsersInterests.FirstOrDefault(x => x.IdUserInterest == id);
            if (userInterest == null)
            {
                throw new Exception("Invalid user interest param");
            }

            if (model.Amount <= 0)
            {
                throw new Exception("Invalid amount");
            }

            if (model.Interest <= 0)
            {
                throw new Exception("Invalid interest");
            }

            userInterest.OriginalAmount = model.Amount;
            userInterest.Interest = model.Interest;
            userInterest.Note = model.Note;

            _db.SaveChanges();
        }

        public void DeleteInterest(int id)
        {
            throw new NotImplementedException();
        }

        public async Task CalculateUsersInterests()
        {
            var activeInterests = _db
                .UsersInterests
                .Include(x => x.InterestPayout)
                .Include(x => x.Cryptocurrency)
                .Where(x => !x.DeletedAt.HasValue);

            var cryptosUsed = await activeInterests
                .Select(x => x.IdCryptocurrency)
                .Distinct()
                .ToListAsync();

            foreach (var idCrypto in cryptosUsed)
            {
                var thisCryptoInterest = await activeInterests.Where(x => x.IdCryptocurrency == idCrypto).ToListAsync();

                var realizedInterests = new List<TUserRealizedInterest>();

                foreach (var userInterest in thisCryptoInterest) 
                {
                    var calculated = InterestCalculator.CalculateCompoundingInterest(userInterest.CompoundedAmount, userInterest.Interest, userInterest.InterestPayout.Code);

                    userInterest.CompoundedAmount = calculated.CompoundedAmount;

                    realizedInterests.Add(new TUserRealizedInterest 
                    {
                        IdUser = userInterest.IdUser,
                        IdUserInterest = userInterest.IdUserInterest,
                        TotalAmount = userInterest.CompoundedAmount,
                        GainedAmount = calculated.GainedInterest,
                        Interest = userInterest.Interest,
                        Date = DateTime.UtcNow.Date,
                        InsertedAt = DateTime.UtcNow
                    });
                }

                await _db.UsersRealizedInterests.AddRangeAsync(realizedInterests);
                await _db.SaveChangesAsync();

                realizedInterests.Clear();
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
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

        public async Task<UserCryptosInterestsInformationModel> GetUserCryptosInterestsInformation(int idUser)
        {
            var user = await _db
                .Users
                .FirstOrDefaultAsync(x => x.IdUser == idUser);

            if (user == null) 
            {
                throw new Exception("IdUser is not valid.");
            }

            var userCryptosInterests = _db
                .UsersInterests
                .Include(x => x.Cryptocurrency)
                .Where(x => x.IdUser == idUser)
                .AsQueryable();

            var groupedCryptosInterests = await userCryptosInterests
                .GroupBy(x => new { x.IdCryptocurrency })
                .Select(x => new UserCrypoInterestInformationModel
                {
                    IdCryptocurrency = x.Key.IdCryptocurrency,
                    CoinMarketCapId = x.Select(y => y.Cryptocurrency.CoinMarketCapId).First(),
                    Code = x.Select(y => y.Cryptocurrency.Code).First(),
                    Name = x.Select(y => y.Cryptocurrency.Name).First(),
                    Price = x.Select(y => y.Cryptocurrency.Price).First(),
                    CompoundedAmount = x.Sum(y => y.CompoundedAmount),
                    AverageInterestRate = InterestCalculator.CalculateAverageInterestRate(x.Select(y => new Tuple<double, double>(y.CompoundedAmount, y.InterestRate).ToValueTuple()).ToList()),
                    DifferrentIntervalsInterest = new DifferentIntervalsInterestModel { }
                })
                .ToListAsync();

            var intervalsEarnings = new DifferentIntervalsInterestsEarningsModel();

            List<string> intervalsToCalculate = new() { CalculationInterval.Daily, CalculationInterval.Weekly, CalculationInterval.Monthly, CalculationInterval.Yearly };

            foreach (var interestInterval in intervalsToCalculate) 
            {
                double summedPossibleInterest = 0;
                foreach (var cryptoInterest in groupedCryptosInterests)
                {
                    var calculatedCryptoIntervalInterest = InterestCalculator.CalculateCompoundingInterest(cryptoInterest.CompoundedAmount, cryptoInterest.AverageInterestRate, interestInterval);

                    var possibleInterest = calculatedCryptoIntervalInterest.GainedInterest * cryptoInterest.Price;

                    switch (interestInterval)
                    {
                        case CalculationInterval.Daily: cryptoInterest.DifferrentIntervalsInterest.Daily = possibleInterest; break;
                        case CalculationInterval.Weekly: cryptoInterest.DifferrentIntervalsInterest.Weekly = possibleInterest; break;
                        case CalculationInterval.Monthly: cryptoInterest.DifferrentIntervalsInterest.Monthly = possibleInterest; break;
                        case CalculationInterval.Yearly: cryptoInterest.DifferrentIntervalsInterest.Yearly = possibleInterest; break;
                        default: throw new Exception("Interval you are trying to calculate is not available");
                    }

                    summedPossibleInterest += possibleInterest;
                }

                switch (interestInterval) 
                {
                    case CalculationInterval.Daily: intervalsEarnings.Daily = summedPossibleInterest; break;
                    case CalculationInterval.Weekly: intervalsEarnings.Weekly = summedPossibleInterest; break;
                    case CalculationInterval.Monthly: intervalsEarnings.Monthly = summedPossibleInterest; break;
                    case CalculationInterval.Yearly: intervalsEarnings.Yearly = summedPossibleInterest; break;
                    default: throw new Exception("Interval you are trying to calculate is not available");
                }
            }

            return new UserCryptosInterestsInformationModel
            {
                CryptosInterests = groupedCryptosInterests,
                DifferentIntervalsInterestsEarnings = intervalsEarnings
            };
        }

        public async Task InsertUserInterest(InsertUserInterestModel model)
        {
            var cryptocurrency = await _db.Cryptocurrencies.FirstOrDefaultAsync(x => x.IdCryptocurrency == model.IdCryptoCurrency);
            if (cryptocurrency == null) 
            {
                throw new UserException("Invalid crypocurrency param");
            }

            var interestPayout = await _db.InterestPayouts.FirstOrDefaultAsync(x => x.IdInterestPayout == model.IdInterestPayout);
            if (interestPayout == null) 
            {
                throw new UserException("Invalid interest payout param");
            }

            var service = await _db.Services.FirstOrDefaultAsync(x => x.IdService == model.IdService);
            if (service == null)
            {
                throw new UserException("Invalid service param");
            }

            if (model.Amount <= 0) 
            {
                throw new UserException("Invalid amount");
            }

            if (model.InterestRate <= 0) 
            {
                throw new UserException("Invalid interest");
            }

            var interest = new TUserInterest
            {
                IdCryptocurrency = cryptocurrency.IdCryptocurrency,
                IdUser = _authService.GetLoggedInUser().IdUser,
                IdInterestPayout = model.IdInterestPayout,
                IdService = model.IdService,
                OriginalAmount = model.Amount,
                CompoundedAmount = model.Amount,
                InterestRate = model.InterestRate,
                Note = model.Note
            };

            await _db.UsersInterests.AddAsync(interest);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateUserInterest(int id, UpdateUserInterestModel model)
        {
            var userInterest = await _db.UsersInterests.FirstOrDefaultAsync(x => x.IdUserInterest == id);
            if (userInterest == null)
            {
                throw new UserException("Invalid idUserInterest param");
            }

            if (model.Amount <= 0)
            {
                throw new UserException("Invalid amount");
            }
            
            if (model.InterestRate <= 0)
            {
                throw new UserException("Invalid interest");
            }

            userInterest.OriginalAmount = model.Amount;
            userInterest.InterestRate = model.InterestRate;
            userInterest.Note = model.Note;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteUserInterest(int id) 
        {
            var idUser = _authService
                .GetLoggedInUser()
                .IdUser;

            var entity = _db
                .UsersInterests
                .SingleOrDefault(x => x.IdUserInterest == id);

            if (entity == null)
            {
                throw new UserException("Invalid idUserInterest param");
            }

            if (entity.IdUser != idUser) 
            {
                throw new UserException("You cannot remove other user interests");
            }

            entity.DeletedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
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
                    var calculated = InterestCalculator.CalculateCompoundingInterest(userInterest.CompoundedAmount, userInterest.InterestRate, userInterest.InterestPayout.Code);

                    userInterest.CompoundedAmount = calculated.CompoundedAmount;

                    realizedInterests.Add(new TUserRealizedInterest 
                    {
                        IdUser = userInterest.IdUser,
                        IdUserInterest = userInterest.IdUserInterest,
                        TotalAmount = userInterest.CompoundedAmount,
                        GainedAmount = calculated.GainedInterest,
                        InterestRate = userInterest.InterestRate,
                        Date = DateTime.UtcNow.Date,
                        InsertedAt = DateTime.UtcNow
                    });
                }

                await _db.UsersRealizedInterests.AddRangeAsync(realizedInterests);
                await _db.SaveChangesAsync();

                realizedInterests.Clear();
            }
        }

        public async Task<List<UserCryptoInputsModel>> GetUserCryptocurrencyInputs(int idUser, int idCryptocurrency) 
        {
            var user = _db.Users.SingleOrDefault(x => x.IdUser == idUser);

            if (user == null)
            {
                throw new UserException("Invalid user param");
            }

            var cryptocurrency = _db.Cryptocurrencies.SingleOrDefault(x => x.IdCryptocurrency == idCryptocurrency);

            if (cryptocurrency == null)
            {
                throw new UserException("Invalid cryptocurrency param");
            }

            var cryptoInputs = _db
              .UsersInterests
              .Include(x => x.InterestPayout)
              .Include(x => x.Cryptocurrency)
              .Include(x => x.Service)
              .Where(x => 
                !x.DeletedAt.HasValue &&
                x.IdCryptocurrency == idCryptocurrency &&
                x.IdUser == idUser
              );

            return  await cryptoInputs
              .Select(x => new UserCryptoInputsModel 
              {
                IdUserInterest = x.IdUserInterest,
                IdCryptocurrency = x.IdCryptocurrency,
                InterestPayout = new InterestPayoutModel 
                {
                    IdInterestPayout = x.IdInterestPayout,
                    Code = x.InterestPayout.Code,
                    Title = x.InterestPayout.Title
                },
                Service = new ServiceModel 
                {
                    IdService = x.Service.IdService,
                    Code = x.Service.Code,
                    Title = x.Service.Title
                },
                Amount = x.OriginalAmount,
                Rate = x.InterestRate,
                Note = x.Note,
                InsertedAt = x.InsertedAt
              })
              .ToListAsync();

        }
    }
}

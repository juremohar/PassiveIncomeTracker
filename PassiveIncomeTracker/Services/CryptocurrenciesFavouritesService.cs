using PassiveIncomeTracker.ApiModels;
using PassiveIncomeTracker.DbModels;
using PassiveIncomeTracker.Interfaces;
using PassiveIncomeTracker.Models;
using PassiveIncomeTracker.OutsideServices;

namespace PassiveIncomeTracker.Services
{
    public class CryptocurrenciesFavouritesService : ICryptocurrenciesFavouritesService
    {
        private readonly DbApi _db;
        private readonly IAuthService _authService;

        public CryptocurrenciesFavouritesService
        (
            DbApi db,
            IAuthService authService
        )
        {
            _db = db;
            _authService = authService;
        }

        public void Follow(int idCryptocurrency)
        {
            var cryptocurrency = _db.Cryptocurrencies.SingleOrDefault(x => x.IdCryptocurrency == idCryptocurrency);
            if (cryptocurrency == null)
                throw new UserException("Invalid param - idCryptocurrency");

            var user = _authService.GetLoggedInUser();
            if (user == null)
                throw new UserException("Invalid user");

            var userFavouriteCryptocurrency = _db
                .UserFavouriteCryptocurrencies
                .SingleOrDefault(x => x.IdUser == user.IdUser && x.IdCryptocurrency == idCryptocurrency);

            if (userFavouriteCryptocurrency != null)
                throw new UserException("You are already following this cryptocurrency");

            var entity = new TUserFavouriteCryptocurrency
            {
                IdCryptocurrency = idCryptocurrency,
                IdUser = user.IdUser
            };

            _db.Add(entity);
            _db.SaveChanges();
        }

        public void Unfollow(int idCryptocurrency)
        {
            var cryptocurrency = _db.Cryptocurrencies.SingleOrDefault(x => x.IdCryptocurrency == idCryptocurrency);
            if (cryptocurrency == null)
                throw new UserException("Invalid param - idCryptocurrency");

            var user = _authService.GetLoggedInUser();
            if (user == null)
                throw new UserException("Invalid user");

            var userFavouriteCryptocurrency = _db
                .UserFavouriteCryptocurrencies
                .SingleOrDefault(x => x.IdUser == user.IdUser && x.IdCryptocurrency == idCryptocurrency);

            if (userFavouriteCryptocurrency == null)
                throw new UserException("You are already not following this cryptocurrency");

            _db.Remove(userFavouriteCryptocurrency);
            _db.SaveChanges();
        }

        public List<int> GetUserFavourites() 
        {
            var user = _authService.GetLoggedInUser();
            if (user == null)
                throw new UserException("Invalid user");

            return _db
                .UserFavouriteCryptocurrencies
                .Where(x => x.IdUser == user.IdUser)
                .Select(x => x.IdCryptocurrency)
                .ToList();
        }
    }
}

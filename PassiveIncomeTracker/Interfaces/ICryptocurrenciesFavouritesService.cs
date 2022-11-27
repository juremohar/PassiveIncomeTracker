
namespace PassiveIncomeTracker.Interfaces
{
    public interface ICryptocurrenciesFavouritesService
    {
        void Follow(int idCryptocurrency);
        void Unfollow(int idCryptocurrency);
        List<int> GetUserFavourites();
    }
}

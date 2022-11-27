using Microsoft.AspNetCore.Mvc;
using PassiveIncomeTracker.ApiModels;
using PassiveIncomeTracker.DbModels;
using PassiveIncomeTracker.Interfaces;
using PassiveIncomeTracker.Models;

namespace PassiveIncomeTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptocurrencyFavouriteController : ControllerBase
    {
        private readonly ICryptocurrenciesFavouritesService _cryptocurrenciesFavouritesService;

        public CryptocurrencyFavouriteController
        (
            ICryptocurrenciesFavouritesService cryptocurrenciesFavouritesService
        ) 
        {
            _cryptocurrenciesFavouritesService = cryptocurrenciesFavouritesService; 
        }


        [HttpPatch("follow/{idCryptocurrency}")]
        public void Follow(int idCryptocurrency)
        {
            _cryptocurrenciesFavouritesService.Follow(idCryptocurrency);
        }

        [HttpPatch("unfollow/{idCryptocurrency}")]
        public void Unfollow(int idCryptocurrency)
        {
            _cryptocurrenciesFavouritesService.Unfollow(idCryptocurrency);
        }
    }
}

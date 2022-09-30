using Microsoft.AspNetCore.Mvc;
using PassiveIncomeTracker.ApiModels;
using PassiveIncomeTracker.DbModels;
using PassiveIncomeTracker.Interfaces;
using PassiveIncomeTracker.Models;

namespace PassiveIncomeTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptocurrenciesController : ControllerBase
    {
        private readonly ICryptocurrenciesService _cryptocurrenciesService;

        public CryptocurrenciesController
        (
            ICryptocurrenciesService cryptocurrenciesService
        ) 
        {
            _cryptocurrenciesService = cryptocurrenciesService; 
        }

        [HttpGet("updateCryptoWithLatestData")]
        public async Task UpdateCryptoWithLatestData()
        {
            await _cryptocurrenciesService.UpdateCryptoWithLatestData();
        }

        [HttpPost]
        public void Post([FromBody] InsertCryptocurrencyModel model)
        {
            _cryptocurrenciesService.Insert(model);
        }

        [HttpGet]
        public List<CryptocurrencyModel> Get([FromQuery] GetCryptocurrenciesFilterModel model)
        {
            return _cryptocurrenciesService.Get(model);
        }

        [HttpGet("{idCryptocurrency}")]
        public CryptocurrencyModel GetById(int idCryptocurrency)
        {
            return _cryptocurrenciesService.GetById(idCryptocurrency);
        }
    }
}

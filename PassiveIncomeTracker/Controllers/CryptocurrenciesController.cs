using Microsoft.AspNetCore.Mvc;
using PassiveIncomeTracker.ApiModels;
using PassiveIncomeTracker.Interfaces;

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

        // GET: api/<CryptocurrenciesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CryptocurrenciesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CryptocurrenciesController>
        [HttpPost]
        public void Post([FromBody] InsertCryptocurrencyModel model)
        {
            _cryptocurrenciesService.Insert(model);
        }

        // PUT api/<CryptocurrenciesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CryptocurrenciesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

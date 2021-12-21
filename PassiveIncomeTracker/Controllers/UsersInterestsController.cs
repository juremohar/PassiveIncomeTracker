using Microsoft.AspNetCore.Mvc;
using PassiveIncomeTracker.ApiModels;
using PassiveIncomeTracker.Interfaces;
using PassiveIncomeTracker.Models;

namespace PassiveIncomeTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersInterestsController : ControllerBase
    {
        private readonly IUserInterestService _userInterestService;

        public UsersInterestsController
        (
            IUserInterestService userInterestService
        ) 
        {
            _userInterestService = userInterestService; 
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] InsertInterestModel model)
        {
            _userInterestService.InsertInterest(model);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UpdateInterestModel model)
        {
            _userInterestService.UpdateInterest(id, model);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

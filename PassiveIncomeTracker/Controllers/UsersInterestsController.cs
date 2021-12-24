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
    }
}

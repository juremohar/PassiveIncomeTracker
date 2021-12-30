using Microsoft.AspNetCore.Mvc;
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
        public async Task Post([FromBody] InsertUserInterestModel model)
        {
            await _userInterestService.InsertUserInterest(model);
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] UpdateUserInterestModel model)
        {
            await _userInterestService.UpdateUserInterest(id, model);
        }

        [HttpGet("UserCryptoBalance/{idUser}")]
        public async Task<UserCryptosInterestsInformationModel> GetUserCryptoBalance(int idUser) 
        {
            return await _userInterestService.GetUserCryptosInterestsInformation(idUser);
        }

        [HttpGet("Calculate")]
        public async Task CalculateUserInterestsAsync()
        {
            await _userInterestService.CalculateUsersInterests();
        }
    }
}

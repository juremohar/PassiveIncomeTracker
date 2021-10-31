using Microsoft.AspNetCore.Mvc;
using PassiveIncomeTracker.ApiModels;
using PassiveIncomeTracker.Interfaces;

namespace PassiveIncomeTracker.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController
        (
            IUserService userService
        ) 
        {
            _userService = userService; 
        }

        [HttpPost("login")]
        public string Login([FromBody] UserLoginModel model)
        {
            return _userService.Login(model);
        }

        [HttpPost("register")]
        public bool Register([FromBody] UserRegisterModel model)
        {
            return _userService.Register(model);
        }
    }
}

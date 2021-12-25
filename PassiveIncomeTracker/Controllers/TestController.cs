using Microsoft.AspNetCore.Mvc;
using PassiveIncomeTracker.Interfaces;
using PassiveIncomeTracker.Models;

namespace PassiveIncomeTracker.Controllers
{
    [Route("api")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IAuthService _authService;

        public TestController
        (
            IAuthService authService
        ) 
        {
            _authService = authService; 
        }

        [HttpGet("health")]
        public string Health()
        {
            return "We up and running!";
        }

        [HttpGet("currentUser")]
        public LoggedInUserModel CurrentUser()
        {
            return _authService.GetLoggedInUser();
        }
    }
}

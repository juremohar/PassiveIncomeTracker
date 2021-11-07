using PassiveIncomeTracker.DbModels;
using PassiveIncomeTracker.Interfaces;
using PassiveIncomeTracker.Models;

namespace PassiveIncomeTracker.Services
{
    public class AuthService : IAuthService
    {
        private readonly DbApi _db;
        private readonly IJwtRepository _jwtRepository; 
        private LoggedInUserModel _userDetails = null;

        public AuthService
        (
            IHttpContextAccessor httpContextAccessor,
            IJwtRepository jwtRepository,   
            DbApi db
        ) 
        {
            _db = db;
            _jwtRepository = jwtRepository; 

            var context = httpContextAccessor.HttpContext;

            string token = null;

            if (context.Request.Headers.TryGetValue("Authorization", out var content))
            {
                token = content.ToString().Substring("Bearer ".Length);
            }
            else if (context.Request.Cookies.TryGetValue("AccountLoginToken", out var cookie)) 
            {
                token = cookie.ToString();
            }

            if (token != null)
            {
                var session = _db
                    .Sessions
                    .FirstOrDefault(x => x.Token == token && !x.RevokedAt.HasValue);

                if (session != null) 
                {
                    _userDetails = _jwtRepository.Decode(token);
                }
            }
        }

        public LoggedInUserModel GetLoggedInUser()
        {
            return _userDetails;
        }
    }
}

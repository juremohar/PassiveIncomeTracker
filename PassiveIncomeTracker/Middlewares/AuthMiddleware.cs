using PassiveIncomeTracker.DbModels;
using PassiveIncomeTracker.Interfaces;

namespace PassiveIncomeTracker.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUserService _jwt;

        // Dependency Injection
        public AuthMiddleware
        (
            RequestDelegate next,
            IUserService jwt
        )
        {
            _next = next;
            _jwt = jwt;
        }

        public async Task Invoke(HttpContext context)
        {
            //Reading the AuthHeader which is signed with JWT
            string authHeader = context.Request.Headers["Authorization"];

            string token = null;

            if (context.Request.Headers.TryGetValue("Authorization", out var content))
            {
                token = content.ToString().Substring("Bearer ".Length);
            }
            else if (context.Request.Cookies.TryGetValue("AccountLoginToken", out var cookie))
            {
                token = cookie.ToString();
            }

            //if (token != null)
            //{
            //    var session = _db
            //        .Sessions
            //        .FirstOrDefault(x => x.Token == token);

            //    if (session != null)
            //    {
            //        _userDetails = _jwtRepository.Decode(token);
            //    }
            //}

            //if (authHeader != null)
            //{
            //    string jwt = authHeader.Replace("Bearer ", "");
            //}

            //Pass to the next middleware
            await _next(context);
        }
    }
}

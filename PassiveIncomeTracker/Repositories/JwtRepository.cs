using Jose;
using Newtonsoft.Json;
using PassiveIncomeTracker.Interfaces;
using PassiveIncomeTracker.Models;
using System.Text;

namespace PassiveIncomeTracker.Repositories
{
    public class JwtRepository : IJwtRepository 
    {
        private readonly IConfiguration _configuration;
        private readonly byte[] _secretKey;

        public JwtRepository
        (
            IConfiguration configuration
        )
        {
            _configuration = configuration;

            var secret = _configuration.GetSection("JwtSecret").Value;
            _secretKey = Encoding.ASCII.GetBytes(secret);
        }

        public LoggedInUserModel Decode(string jwt)
        {
            var decoded = JWT.Decode(jwt, _secretKey);

            return JsonConvert.DeserializeObject<LoggedInUserModel>(decoded);
        }

        public string Encode(LoggedInUserModel model)
        {
            string payload = JsonConvert.SerializeObject(model);

            return JWT.Encode(payload, _secretKey, JweAlgorithm.A256KW, JweEncryption.A256CBC_HS512);
        }
    }
}

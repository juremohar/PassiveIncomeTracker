using PassiveIncomeTracker.Models;

namespace PassiveIncomeTracker.Interfaces
{
    public interface IJwtRepository
    {
        public string Encode(LoggedInUserModel model);
        public LoggedInUserModel Decode(string jwt);
    }
}

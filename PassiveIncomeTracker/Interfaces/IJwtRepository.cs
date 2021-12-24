using PassiveIncomeTracker.Models;

namespace PassiveIncomeTracker.Interfaces
{
    public interface IJwtRepository
    {
        string Encode(LoggedInUserModel model);
        LoggedInUserModel Decode(string jwt);
    }
}

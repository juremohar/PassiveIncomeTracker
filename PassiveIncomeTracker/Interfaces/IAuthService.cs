using PassiveIncomeTracker.Models;

namespace PassiveIncomeTracker.Interfaces
{
    public interface IAuthService
    {
        LoggedInUserModel GetLoggedInUser();
    }
}

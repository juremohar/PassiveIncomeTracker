using PassiveIncomeTracker.ApiModels;

namespace PassiveIncomeTracker.Interfaces
{
    public interface IUserService
    {
        string Login(UserLoginModel model);
        bool Register(UserRegisterModel model);
        void Logout(string token);
    }
}

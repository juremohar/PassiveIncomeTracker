namespace PassiveIncomeTracker.ApiModels
{
    public class UserLoginModel 
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserRegisterModel 
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordRepeat { get; set; }
    }
}

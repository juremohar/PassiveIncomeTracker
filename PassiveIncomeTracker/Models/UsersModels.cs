namespace PassiveIncomeTracker.Models
{
    public class LoggedInUserModel 
    {
        public int IdUser { get;set; }
        public string Email { get; set; }
        public DateTime ExpirationDate { get; set; } = DateTime.Now.AddMonths(1);
    }
}

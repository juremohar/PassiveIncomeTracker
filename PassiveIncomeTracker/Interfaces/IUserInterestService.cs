using PassiveIncomeTracker.ApiModels;
using PassiveIncomeTracker.Models;

namespace PassiveIncomeTracker.Interfaces
{
    public interface IUserInterestService
    {
        public void InsertInterest(InsertInterestModel model);
        public void UpdateInterest(int id, UpdateInterestModel model);  
        public void DeleteInterest(int id);
        public List<InterestModel> GetInterests(GetInterestFilterModel model); // TODO: this needs to be more generic  
        public InterestModel GetInterestById(int id);
    }
}

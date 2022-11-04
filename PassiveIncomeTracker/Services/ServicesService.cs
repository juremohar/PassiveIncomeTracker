using PassiveIncomeTracker.DbModels;
using PassiveIncomeTracker.Interfaces;
using PassiveIncomeTracker.Models;

namespace PassiveIncomeTracker.Services
{
    public class ServicesService : IServicesService
    {
        private readonly DbApi _db;


        public ServicesService
        (
            DbApi db
        )
        {
            _db = db;
        }

        public List<ServiceModel> GetServices()
        {
            return _db
                .Services
                .Select(x => new ServiceModel
                {
                    IdService = x.IdService,
                    Code = x.Code,
                    Title = x.Title
                })
                .OrderBy(x => x.Title)
                .ToList();

        }
    }
}

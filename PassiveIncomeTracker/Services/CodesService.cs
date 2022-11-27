using PassiveIncomeTracker.DbModels;
using PassiveIncomeTracker.Interfaces;
using PassiveIncomeTracker.Models;

namespace PassiveIncomeTracker.Services
{
    public class CodesService : ICodesService
    {
        private readonly DbApi _db;

        public CodesService
        (
            DbApi db
        )
        {
            _db = db;
        }

        public List<CountryModel> GetCountries()
        {
            return _db
               .Countries
               .Select(x => new CountryModel
               {
                   IdCountry = x.IdCountry,
                   Short = x.Short,
                   Name = x.Name
               })
               .OrderBy(x => x.Name)
               .ToList();
        }

        public List<InterestPayoutModel> GetInterestsPayoutes()
        {
            return _db
                .InterestPayouts
                .Select(x => new InterestPayoutModel
                {
                    IdInterestPayout = x.IdInterestPayout,
                    Code = x.Code,
                    Title = x.Title
                })
                .OrderBy(x => x.Title)
                .ToList();
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

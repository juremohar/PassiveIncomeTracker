using Microsoft.AspNetCore.Mvc;
using PassiveIncomeTracker.Interfaces;
using PassiveIncomeTracker.Models;

namespace PassiveIncomeTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodesController : ControllerBase
    {
        private readonly ICodesService _codesService;

        public CodesController
        (
            ICodesService codesService
        ) 
        {
            _codesService = codesService; 
        }


        [HttpGet("services")]
        public List<ServiceModel> GetServices()
        {
            return _codesService.GetServices();
        }

        [HttpGet("interestsPayouts")]
        public List<InterestPayoutModel> GetInterestsPayoutes()
        {
            return _codesService.GetInterestsPayoutes();
        }

        [HttpGet("countries")]
        public List<CountryModel> GetCountries()
        {
            return _codesService.GetCountries();
        }
    }
}

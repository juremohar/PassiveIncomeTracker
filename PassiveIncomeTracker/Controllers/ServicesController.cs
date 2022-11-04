using Microsoft.AspNetCore.Mvc;
using PassiveIncomeTracker.Interfaces;
using PassiveIncomeTracker.Models;

namespace PassiveIncomeTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesService _servicesService;

        public ServicesController
        (
            IServicesService servicesService
        ) 
        {
            _servicesService = servicesService; 
        }


        [HttpGet]
        public List<ServiceModel> Get()
        {
            return _servicesService.GetServices();
        }
    }
}

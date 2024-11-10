using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KoiShip.Service;
using KoiShip.Service.Base;
using KoiShip_DB.Data.Models;

namespace KoiShip.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingsController : ControllerBase
    {
        private readonly IPricingService _PricingService;

        public PricingsController(IPricingService PricingService)
        {
            _PricingService = PricingService;
        }

        // GET: api/Pricings
        [HttpGet]
        public async Task<IBusinessResult> GetPricings()
        {
            return await _PricingService.GetALLPricing();
        }

        // GET: api/Pricings/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetPricing(int id)
        {
            return await _PricingService.GetPricingById(id);
        }

        // PUT: api/Pricings/5
        [HttpPut("{id}")]
        public async Task<IBusinessResult> PutPricing(Pricing Pricing)
        {


            var result = await _PricingService.UpdatePricing(Pricing);

            return result;
        }

        // POST: api/Pricings
        [HttpPost]
        public async Task<IBusinessResult> PostPricing(Pricing Pricing)
        {
            var result = await _PricingService.CreatePricing(Pricing);


            return result;
        }

        // DELETE: api/Pricings/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeletePricing(int id)
        {
            var result = await _PricingService.DeletePricing(id);


            return result;
        }
    }
}

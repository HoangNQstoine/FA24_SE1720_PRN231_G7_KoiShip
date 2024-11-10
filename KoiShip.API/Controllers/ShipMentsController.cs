using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KoiShip.Service;
using KoiShip.Service.Base;
using KoiShip_DB.Data.Models;

namespace KoiShip.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipMentsController : ControllerBase
    {
        private readonly IShipMentService _ShipMentService;

        public ShipMentsController(IShipMentService ShipMentService)
        {
            _ShipMentService = ShipMentService;
        }

        // GET: api/ShipMents
        [HttpGet]
        public async Task<IBusinessResult> GetShipMents()
        {
            return await _ShipMentService.GetALLShipMent();
        }

        // GET: api/ShipMents/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetShipMent(int id)
        {
            return await _ShipMentService.GetShipMentById(id);
        }

        // PUT: api/ShipMents/5
        [HttpPut("{id}")]
        public async Task<IBusinessResult> PutShipMent(ShipMent ShipMent)
        {


            var result = await _ShipMentService.UpdateShipMent(ShipMent);

            return result;
        }

        // POST: api/ShipMents
        [HttpPost]
        public async Task<IBusinessResult> PostShipMent(ShipMent ShipMent)
        {
            var result = await _ShipMentService.CreateShipMent(ShipMent);


            return result;
        }

        // DELETE: api/ShipMents/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteShipMent(int id)
        {
            var result = await _ShipMentService.DeleteShipMent(id);


            return result;
        }
    }
}

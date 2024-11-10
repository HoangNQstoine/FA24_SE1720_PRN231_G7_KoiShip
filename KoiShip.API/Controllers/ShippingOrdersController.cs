using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KoiShip.Service;
using KoiShip.Service.Base;
using KoiShip_DB.Data.Models;
using KoiShip_DB.Data.DTO;
namespace KoiShip.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingOrdersController : ControllerBase
    {
        private readonly IShippingOrderService _shippingOrderService;

        public ShippingOrdersController(IShippingOrderService shippingOrderService)
        {
            _shippingOrderService = shippingOrderService;
        }

        // GET: api/ShippingOrders
        [HttpGet]
        public async Task<IBusinessResult> GetShippingOrders()
        {
            return await _shippingOrderService.GetALLShippingOrder();
        }

        // GET: api/ShippingOrders/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetShippingOrder(int id)
        {
            return await _shippingOrderService.GetShippingOrderById(id);
        }

        // PUT: api/ShippingOrders/5
        [HttpPut("{id}")]
        public async Task<IBusinessResult> PutShippingOrder(ShippingOrderDTO shippingOrder)
        {
            

            var result = await _shippingOrderService.UpdateShippingOrder( shippingOrder);

            return result;
        }

        // POST: api/ShippingOrders
        [HttpPost]
        public async Task<IBusinessResult> PostShippingOrder([FromBody] ShippingOrderDTO shippingOrder)
        {
            var result = await _shippingOrderService.CreateShippingOrder(shippingOrder);
           

            return result;
        }

        // DELETE: api/ShippingOrders/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteShippingOrder(int id)
        {
            var result = await _shippingOrderService.DeleteShippingOrder(id);
           

            return result;
        }
    }
}

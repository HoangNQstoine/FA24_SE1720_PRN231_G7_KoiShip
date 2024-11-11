using KoiShip.Service;
using KoiShip.Service.Base;
using KoiShip_DB.Data.DTO.Request;
using Microsoft.AspNetCore.Mvc;
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
        //[HttpGet]
        //public async Task<IBusinessResult> GetShippingOrders()
        //{
        //    return await _shippingOrderService.GetALLShippingOrder();
        //}
        [HttpGet]
        public async Task<IBusinessResult> SearchShippingOrders(string? phoneNumber, int? totalPrice)
        {
            return await _shippingOrderService.SearchShippingOrders(phoneNumber, totalPrice);
        }

        // GET: api/ShippingOrders/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetShippingOrder(int id)
        {
            return await _shippingOrderService.GetShippingOrderById(id);
        }

        // PUT: api/ShippingOrders/5
        [HttpPut("{id}")]
        public async Task<IBusinessResult> PutShippingOrder(ShippingOrderEdit shippingOrder)
        {


            var result = await _shippingOrderService.UpdateShippingOrder(shippingOrder);

            return result;
        }

        // POST: api/ShippingOrders
        [HttpPost]
        public async Task<IBusinessResult> PostShippingOrder([FromBody] ShippingOrderRequest shippingOrder)
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

using KoiShip.Service;
using KoiShip.Service.Base;
using KoiShip_DB.Data.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace KoiShip.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KoiFishsController : ControllerBase
    {
        private readonly IKoiFishService _KoiFishService;

        public KoiFishsController(IKoiFishService KoiFishService)
        {
            _KoiFishService = KoiFishService;
        }

        // GET: api/KoiFishs
        [HttpGet]
        public async Task<IBusinessResult> GetKoiFishs(string? Name, int? Age)
        {
            return await _KoiFishService.SearchKoiFish(Name, Age);
        }

        // GET: api/KoiFishs/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetKoiFish(int id)
        {
            return await _KoiFishService.GetKoiFishById(id);
        }

        // PUT: api/KoiFishs/5
        [HttpPut("{id}")]
        public async Task<IBusinessResult> PutKoiFish(KoiFishEdit KoiFish)
        {


            var result = await _KoiFishService.UpdateKoiFish(KoiFish);

            return result;
        }

        // POST: api/KoiFishs
        [HttpPost]
        public async Task<IBusinessResult> PostKoiFish(KoiFishCreate KoiFish)
        {
            var result = await _KoiFishService.CreateKoiFish(KoiFish);


            return result;
        }

        // DELETE: api/KoiFishs/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteKoiFish(int id)
        {
            var result = await _KoiFishService.DeleteKoiFish(id);


            return result;
        }
    }
}

using KoiShip.Service.Base;
using KoiShip.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiShip.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorysController : ControllerBase
    {
        private readonly ICategoryService _CategoryService;

        public CategorysController(ICategoryService CategoryService)
        {
            _CategoryService = CategoryService;
        }

        // GET: api/Categorys
        [HttpGet]
        public async Task<IBusinessResult> GetCategorys()
        {
            return await _CategoryService.GetALLCategory();
        }
    }
}

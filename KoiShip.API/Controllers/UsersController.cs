using KoiShip.Service.Base;
using KoiShip.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiShip.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _UserService;

        public UsersController(IUserService UserService)
        {
            _UserService = UserService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IBusinessResult> GetUsers()
        {
            return await _UserService.GetALLUser();
        }
    }
}

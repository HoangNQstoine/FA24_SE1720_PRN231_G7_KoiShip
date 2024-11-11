using KoiShip.Service.Base;
using KoiShip_DB.Data;
using KoiShip_DB.Data.Models;
using KoiShip.Common;
using KoiShip.Service;

namespace KoiShip.Service
{
    public interface IUserService
    {
        Task<IBusinessResult> GetALLUser();
    }

    public class UserService : IUserService
    {
        private readonly UnitOfWork _unitOfWork;

        public UserService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetALLUser()
        {
            try
            {
                var result = await _unitOfWork.UsersRepository.GetAllAsync();
                if (result != null)
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
                }
            }
            catch (Exception e)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, e.Message);
            }
        }
    }
}

using KoiShip.Common;
using KoiShip.Service.Base;
using KoiShip_DB.Data;
using KoiShip_DB.Data.DTO.Request;
using KoiShip_DB.Data.Models;

namespace KoiShip.Service
{
    public interface IKoiFishService
    {
        Task<IBusinessResult> GetALLKoiFish();
        Task<IBusinessResult> SaveKoiFish(KoiFish KoiFish);
        Task<IBusinessResult> CreateKoiFish(KoiFishCreate KoiFish);
        Task<IBusinessResult> UpdateKoiFish(KoiFishEdit KoiFish);
        Task<IBusinessResult> DeleteKoiFish(int idKoiFish);
        Task<IBusinessResult> GetKoiFishById(int idKoiFish);
        Task<IBusinessResult> SearchKoiFish(string? Name, int? Age);
    }

    public class KoiFishService : IKoiFishService
    {
        private readonly UnitOfWork _unitOfWork;

        public KoiFishService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> SearchKoiFish(string? Name, int? Age)
        {
            try
            {
                var result = await _unitOfWork.KoiFishsRepository.SearchKoiFish(Name, Age);
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
        public async Task<IBusinessResult> CreateKoiFish(KoiFishCreate request)
        {
            try
            {
                var KoiFish = new KoiFish
                {
                    UserId = request.UserId,
                    CategoryId = request.CategoryId,
                    Name = request.Name,
                    Description = request.Description,
                    Weight = request.Weight,
                    Age = request.Age,
                    ColorPattern = request.ColorPattern,
                    Price = request.Price,
                    UrlImg = request.UrlImg,
                    Status = request.Status,
                };
                var result = await _unitOfWork.KoiFishsRepository.CreateAsync(KoiFish);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, KoiFish);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception e)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, e.Message);
            }
        }

        public async Task<IBusinessResult> DeleteKoiFish(int idKoiFish)
        {
            try
            {
                var KoiFish = await _unitOfWork.KoiFishsRepository.GetByIdAsync(idKoiFish);
                if (KoiFish == null)
                {
                    return new BusinessResult(Const.FAIL_DELETE_CODE, "Shipping order not found.");
                }

                var result = await _unitOfWork.KoiFishsRepository.RemoveAsync(KoiFish);
                if (result)
                {
                    return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                }
            }
            catch (Exception e)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, e.Message);
            }
        }

        public async Task<IBusinessResult> GetALLKoiFish()
        {
            try
            {
                var result = await _unitOfWork.KoiFishsRepository.GetAllAsync();
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

        public async Task<IBusinessResult> GetKoiFishById(int idKoiFish)
        {
            try
            {
                var result = await _unitOfWork.KoiFishsRepository.GetByIdAsync(idKoiFish);
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

        public async Task<IBusinessResult> SaveKoiFish(KoiFish KoiFish)
        {
            try
            {
                var result = -1;
                if (KoiFish != null)
                {
                    result = await _unitOfWork.KoiFishsRepository.CreateAsync(KoiFish);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, KoiFish);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.KoiFishsRepository.UpdateAsync(KoiFish);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, KoiFish);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }

            }
            catch (Exception e)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, e.Message);
            }
        }

        public async Task<IBusinessResult> UpdateKoiFish(KoiFishEdit request)
        {
            try
            {
                var KoiFish = new KoiFish
                {
                    Id = request.Id,
                    UserId = request.UserId,
                    CategoryId = request.CategoryId,
                    Name = request.Name,
                    Description = request.Description,
                    Weight = request.Weight,
                    Age = request.Age,
                    ColorPattern = request.ColorPattern,
                    Price = request.Price,
                    UrlImg = request.UrlImg,
                    Status = request.Status,
                };
                var result = await _unitOfWork.KoiFishsRepository.UpdateAsync(KoiFish);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, KoiFish);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception e)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, e.Message);
            }
        }
    }
}

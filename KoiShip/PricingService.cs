using KoiShip.Service.Base;
using KoiShip_DB.Data;
using KoiShip_DB.Data.Models;
using KoiShip.Common;
using KoiShip.Service;

namespace KoiShip.Service
{
    public interface IPricingService
    {
        Task<IBusinessResult> GetALLPricing();
        Task<IBusinessResult> SavePricing(Pricing Pricing);
        Task<IBusinessResult> CreatePricing(Pricing Pricing);
        Task<IBusinessResult> UpdatePricing(Pricing Pricing);
        Task<IBusinessResult> DeletePricing(int idPricing);
        Task<IBusinessResult> GetPricingById(int idPricing);
    }

    public class PricingService : IPricingService
    {
        private readonly UnitOfWork _unitOfWork;

        public PricingService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> CreatePricing(Pricing Pricing)
        {
            try
            {
                var result = await _unitOfWork.PricingsRepository.CreateAsync(Pricing);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, Pricing);
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

        public async Task<IBusinessResult> DeletePricing(int idPricing)
        {
            try
            {
                var Pricing = await _unitOfWork.PricingsRepository.GetByIdAsync(idPricing);
                if (Pricing == null)
                {
                    return new BusinessResult(Const.FAIL_DELETE_CODE, "Shipping order not found.");
                }

                var result = await _unitOfWork.PricingsRepository.RemoveAsync(Pricing);
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

        public async Task<IBusinessResult> GetALLPricing()
        {
            try
            {
                var result = await _unitOfWork.PricingsRepository.GetAllAsync();
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

        public async Task<IBusinessResult> GetPricingById(int idPricing)
        {
            try
            {
                var result = await _unitOfWork.PricingsRepository.GetByIdAsync(idPricing);
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

        public async Task<IBusinessResult> SavePricing(Pricing Pricing)
        {
            try
            {
                var result = -1;
                if (Pricing != null)
                {
                    result = await _unitOfWork.PricingsRepository.CreateAsync(Pricing);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, Pricing);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.PricingsRepository.UpdateAsync(Pricing);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, Pricing);
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

        public async Task<IBusinessResult> UpdatePricing(Pricing Pricing)
        {
            try
            {
                var result = await _unitOfWork.PricingsRepository.UpdateAsync(Pricing);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, Pricing);
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

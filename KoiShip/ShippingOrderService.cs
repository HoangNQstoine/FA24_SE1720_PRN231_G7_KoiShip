using KoiShip.Service.Base;
using KoiShip_DB.Data;
using KoiShip_DB.Data.Models;
using KoiShip.Common;
using KoiShip.Service;
using KoiShip_DB.Data.DTO.Request;
using Azure.Core;

namespace KoiShip.Service
{
    public interface IShippingOrderService
    {
        Task<IBusinessResult> GetALLShippingOrder();
        Task<IBusinessResult> SaveShippingOrder(ShippingOrder shippingOrder);
        Task<IBusinessResult> CreateShippingOrder(ShippingOrderRequest shippingOrder);
        Task<IBusinessResult> UpdateShippingOrder(ShippingOrderEdit shippingOrder);
        Task<IBusinessResult> DeleteShippingOrder(int idShippingOrder);
        Task<IBusinessResult> GetShippingOrderById(int idShippingOrder);
    }

    public class ShippingOrderService : IShippingOrderService
    {
        private readonly UnitOfWork _unitOfWork;

        public ShippingOrderService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> CreateShippingOrder(ShippingOrderRequest request)
        {
            try
            {
                var shippingOrder = new ShippingOrder
                {
                    UserId = request.UserId,
                    PricingId = request.PricingId,
                    ShipMentId = request.ShipMentId,
                    AdressTo = request.AdressTo,
                    PhoneNumber = request.PhoneNumber,
                    Description = request.Description,
                    TotalPrice = request.TotalPrice,
                    OrderDate = request.OrderDate,
                    ShippingDate = request.ShippingDate,
                    EstimatedDeliveryDate = request.EstimatedDeliveryDate
                };
                var result = await _unitOfWork.ShippingOrdersRepository.CreateAsync(shippingOrder);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, shippingOrder);
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

        public async Task<IBusinessResult> DeleteShippingOrder(int idShippingOrder)
        {
            try
            {
                var shippingOrder = await _unitOfWork.ShippingOrdersRepository.GetByIdAsync(idShippingOrder);
                if (shippingOrder == null)
                {
                    return new BusinessResult(Const.FAIL_DELETE_CODE, "Shipping order not found.");
                }

                var result = await _unitOfWork.ShippingOrdersRepository.RemoveAsync(shippingOrder);
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

        public async Task<IBusinessResult> GetALLShippingOrder()
        {
            try
            {
                var result = await _unitOfWork.ShippingOrdersRepository.GetAllAsync();
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

        public async Task<IBusinessResult> GetShippingOrderById(int idShippingOrder)
        {
            try
            {
                var result = await _unitOfWork.ShippingOrdersRepository.GetByIdAsync(idShippingOrder);
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

        public async Task<IBusinessResult> SaveShippingOrder(ShippingOrder shippingOrder)
        {
            try
            {
                var result = -1;
                if (shippingOrder != null)
                {
                    result = await _unitOfWork.ShippingOrdersRepository.CreateAsync(shippingOrder);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, shippingOrder);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.ShippingOrdersRepository.UpdateAsync(shippingOrder);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, shippingOrder);
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

        public async Task<IBusinessResult> UpdateShippingOrder(ShippingOrderEdit request)
        {
            try
            {
                var shippingOrder = new ShippingOrder
                {
                    Id = request.Id,
                    UserId = request.UserId,
                    PricingId = request.PricingId,
                    ShipMentId = request.ShipMentId,
                    AdressTo = request.AdressTo,
                    PhoneNumber = request.PhoneNumber,
                    Description = request.Description,
                    TotalPrice = request.TotalPrice,
                    OrderDate = request.OrderDate,
                    ShippingDate = request.ShippingDate,
                    EstimatedDeliveryDate = request.EstimatedDeliveryDate
                };
                var result = await _unitOfWork.ShippingOrdersRepository.UpdateAsync(shippingOrder);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, shippingOrder);
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

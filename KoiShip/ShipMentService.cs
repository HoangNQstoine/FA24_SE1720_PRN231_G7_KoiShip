using KoiShip.Service.Base;
using KoiShip_DB.Data;
using KoiShip_DB.Data.Models;
using KoiShip.Common;
using KoiShip.Service;
using KoiShip_DB.Data.DTO.Request;
using Azure.Core;

namespace KoiShip.Service
{
    public interface IShipMentService
    {
        Task<IBusinessResult> GetALLShipMent();
        Task<IBusinessResult> SaveShipMent(ShipMent ShipMent);
        Task<IBusinessResult> CreateShipMent(ShipMentCreate ShipMent);
        Task<IBusinessResult> UpdateShipMent(ShipMentEdit ShipMent);
        Task<IBusinessResult> DeleteShipMent(int idShipMent);
        Task<IBusinessResult> GetShipMentById(int idShipMent);
    }

    public class ShipMentService : IShipMentService
    {
        private readonly UnitOfWork _unitOfWork;

        public ShipMentService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> CreateShipMent(ShipMentCreate request)
        {
            try
            {
                var ShipMent = new ShipMent
                {
                    UserId = request.UserId,
                    Vehicle = request.Vehicle,
                    EstimatedArrivalDate = request.EstimatedArrivalDate,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    HealthCheck = request.HealthCheck,
                    Description = request.Description,
                    Weight = request.Weight,
                    Status = request.Status,
                };
                var result = await _unitOfWork.ShipMentsRepository.CreateAsync(ShipMent);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, ShipMent);
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

        public async Task<IBusinessResult> DeleteShipMent(int idShipMent)
        {
            try
            {
                var ShipMent = await _unitOfWork.ShipMentsRepository.GetByIdAsync(idShipMent);
                if (ShipMent == null)
                {
                    return new BusinessResult(Const.FAIL_DELETE_CODE, "Shipping order not found.");
                }

                var result = await _unitOfWork.ShipMentsRepository.RemoveAsync(ShipMent);
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

        public async Task<IBusinessResult> GetALLShipMent()
        {
            try
            {
                var result = await _unitOfWork.ShipMentsRepository.GetAllAsync();
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

        public async Task<IBusinessResult> GetShipMentById(int idShipMent)
        {
            try
            {
                var result = await _unitOfWork.ShipMentsRepository.GetByIdAsync(idShipMent);
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

        public async Task<IBusinessResult> SaveShipMent(ShipMent ShipMent)
        {
            try
            {
                var result = -1;
                if (ShipMent != null)
                {
                    result = await _unitOfWork.ShipMentsRepository.CreateAsync(ShipMent);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, ShipMent);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.ShipMentsRepository.UpdateAsync(ShipMent);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, ShipMent);
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

        public async Task<IBusinessResult> UpdateShipMent(ShipMentEdit request)
        {
            try
            {
                var ShipMent = new ShipMent
                {
                    Id = request.Id,
                    UserId = request.UserId,
                    Vehicle = request.Vehicle,
                    EstimatedArrivalDate = request.EstimatedArrivalDate,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    HealthCheck = request.HealthCheck,
                    Description = request.Description,
                    Weight = request.Weight,
                    Status = request.Status,
                };
                var result = await _unitOfWork.ShipMentsRepository.UpdateAsync(ShipMent);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, ShipMent);
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

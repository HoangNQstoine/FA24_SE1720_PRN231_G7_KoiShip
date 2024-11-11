﻿using KoiShip_DB.Data.Base;
using KoiShip_DB.Data.Models;
using Microsoft.EntityFrameworkCore;

public class ShippingOrdersRepository : GenericRepository<ShippingOrder>
{
    public ShippingOrdersRepository(KoiShipDbContext context) : base(context) { }

    public async Task<List<ShippingOrder>> GetAllAsync()
    {
        return await _context.Set<ShippingOrder>()
                             .Include(s => s.Pricing)
                             .Include(s => s.ShipMent)
                             .Include(s => s.User)
                             .ToListAsync();
    }

    public async Task<bool> RemoveAsync(ShippingOrder entity)
    {
        var shippingOrderDetails = await _context.ShippingOrderDetails.Where(s => s.ShippingOrdersId == entity.Id).ToListAsync();
        _context.RemoveRange(shippingOrderDetails);
        _context.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}

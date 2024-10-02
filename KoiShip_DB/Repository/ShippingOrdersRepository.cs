using KoiShip_DB.Data.Base;
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
}

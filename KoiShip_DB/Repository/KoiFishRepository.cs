using KoiShip_DB.Data.Base;
using KoiShip_DB.Data.Models;
using Microsoft.EntityFrameworkCore;

public class KoiFishsRepository : GenericRepository<KoiFish>
{
    public KoiFishsRepository(KoiShipDbContext context) : base(context) { }

    public async Task<List<KoiFish>> GetAllAsync()
    {
        return await _context.Set<KoiFish>()
                             .Include(s => s.Category)
                             .Include(s => s.ShippingOrderDetail)
                             .Include(s => s.User)
                             .ToListAsync();
    }
}

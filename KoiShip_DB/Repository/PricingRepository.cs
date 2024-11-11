using KoiShip_DB.Data.Base;
using KoiShip_DB.Data.Models;
using Microsoft.EntityFrameworkCore;

public class PricingsRepository : GenericRepository<Pricing>
{
    public PricingsRepository(KoiShipDbContext context) : base(context) { }

    public async Task<List<Pricing>> GetAllAsync()
    {
        return await _context.Set<Pricing>()
                             .ToListAsync();
    }
}

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
    public async Task<List<KoiFish>> SearchKoiFish(string? Name, int? Age)
    {
        return await _context.KoiFishes
                             .Include(s => s.Category)
                             .Include(s => s.ShippingOrderDetail)
                             .Include(s => s.User)
                             .Where(ship => (string.IsNullOrEmpty(Name) || ship.Name.Contains(Name)) &&
                                            (!Age.HasValue || ship.Age == Age.Value))
                             .ToListAsync();
    }
}

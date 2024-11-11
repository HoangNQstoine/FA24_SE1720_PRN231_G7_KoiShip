using KoiShip_DB.Data.Base;
using KoiShip_DB.Data.Models;
using Microsoft.EntityFrameworkCore;

public class ShipMentsRepository : GenericRepository<ShipMent>
{
    public ShipMentsRepository(KoiShipDbContext context) : base(context) { }

    public async Task<List<ShipMent>> GetAllAsync()
    {
        return await _context.Set<ShipMent>()
                             .Include(s => s.User)
                             .ToListAsync();
    }
    public async Task<List<ShipMent>> SearchShipMent(string? Vehicle, string? Description)
    {
        return await _context.Set<ShipMent>()
                             .Include(s => s.User)
                             .Where(ment => (string.IsNullOrEmpty(Vehicle) || ment.Vehicle == Vehicle) &&
                                            (string.IsNullOrEmpty(Description) || ment.Description.Contains(Description)))
                             .ToListAsync();
    }


}

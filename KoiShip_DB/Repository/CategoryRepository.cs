using KoiShip_DB.Data.Base;
using KoiShip_DB.Data.Models;
using Microsoft.EntityFrameworkCore;

public class CategorysRepository : GenericRepository<Category>
{
    public CategorysRepository(KoiShipDbContext context) : base(context) { }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Set<Category>()
                             .ToListAsync();
    }
}

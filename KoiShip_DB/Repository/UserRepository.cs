using KoiShip_DB.Data.Base;
using KoiShip_DB.Data.Models;
using Microsoft.EntityFrameworkCore;

public class UsersRepository : GenericRepository<User>
{
    public UsersRepository(KoiShipDbContext context) : base(context) { }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Set<User>()
                             .ToListAsync();
    }
}

using KoiShip_DB.Data.Base;
using KoiShip_DB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShip_DB.Data.Repository
{
    internal class KoiFishRepository : GenericRepository<KoiFish>
    {
        public KoiFishRepository() { }
        public KoiFishRepository(KoiShip_DBContext context) => _context = context;
    } 
}

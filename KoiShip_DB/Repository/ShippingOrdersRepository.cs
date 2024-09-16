using System;
using KoiShip_DB.Data.Base;
using KoiShip_DB.Data.Models;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShip_DB.Data.Repository
{
    public class ShippingOrdersRepository : GenericRepository<ShippingOrder>
    {
        public ShippingOrdersRepository() { }
        public ShippingOrdersRepository(KoiShip_DBContext context) => _context = context;
    }
}

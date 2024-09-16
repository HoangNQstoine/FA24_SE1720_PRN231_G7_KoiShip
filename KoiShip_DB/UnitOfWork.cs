using KoiShip_DB.Data.Models;
using KoiShip_DB.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShip_DB.Data
{
    public class UnitOfWork
    {
        private KoiShip_DBContext context;
        private ShippingOrdersRepository shippingOrdersRepository;

        public UnitOfWork()
        {
            context ??= new KoiShip_DBContext();
        }
        public ShippingOrdersRepository ShippingOrdersRepository
        {

            get  {return shippingOrdersRepository ??= new ShippingOrdersRepository(context); }

           
        }
    }
}
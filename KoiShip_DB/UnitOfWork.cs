using KoiShip_DB.Data.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShip_DB.Data
{
    public class UnitOfWork
    {
        private KoiShipDbContext context;
        private ShippingOrdersRepository shippingOrdersRepository;
        private PricingsRepository pricingsRepository;
        private KoiFishsRepository koiFishsRepository;
        private ShipMentsRepository shipMentsRepository;
        private CategorysRepository categorysRepository;
        private UsersRepository usersRepository;

        public UnitOfWork()
        {
            context ??= new KoiShipDbContext();
        }
        public ShippingOrdersRepository ShippingOrdersRepository
        {

            get  {return shippingOrdersRepository ??= new ShippingOrdersRepository(context); }


        }
        public ShipMentsRepository ShipMentsRepository
        {

            get { return shipMentsRepository ??= new ShipMentsRepository(context); }


        }
        public KoiFishsRepository KoiFishsRepository
        {
            get { return koiFishsRepository ??= new KoiFishsRepository(context); }


        }
        public PricingsRepository PricingsRepository
        {

            get { return pricingsRepository ??= new PricingsRepository(context); }


        }
        public CategorysRepository CategorysRepository
        {

            get { return categorysRepository ??= new CategorysRepository(context); }


        }
        public UsersRepository UsersRepository
        {

            get { return usersRepository ??= new UsersRepository(context); }


        }
    }
}
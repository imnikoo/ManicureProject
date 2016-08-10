using Dataa.EntityFramework.Repositories;
using ManicureDomain.Abstract;
using System.Data.Entity;

namespace Data.EntityFramework.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;

        private ICategoryRepository _categoryRepository;
        private ICityRepository _cityRepository;
        private IClientRepository _clientRepository;
        private IItemRepository _itemRepository;
        private IOrderRepository _orderRepository;
        private IPurchasePlaceRepository _purchasePlaceRepository;
        private IPurchaseRepository _purchaseRepository;

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public UnitOfWork()
        {
            context = new ManicureContext();
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository = new CategoryRepository(context);
                }

                return _categoryRepository;
            }
        }

        public ICityRepository CityRepository
        {
            get
            {
                if (_cityRepository == null)
                {
                    _cityRepository = new CityRepository(context);
                }

                return _cityRepository;
            }
        }

        public IClientRepository ClientRepository
        {
            get
            {
                if (_clientRepository == null)
                {
                    _clientRepository = new ClientRepository(context);
                }

                return _clientRepository;
            }
        }

        public IItemRepository ItemRepository
        {
            get
            {
                if (_itemRepository == null)
                {
                    _itemRepository = new ItemRepository(context);
                }

                return _itemRepository;
            }
        }

        public IOrderRepository OrderRepository
        {
            get
            {
                if (_orderRepository == null)
                {
                    _orderRepository = new OrderRepository(context);
                }

                return _orderRepository;
            }
        }

        public IPurchasePlaceRepository PurchasePlaceRepository
        {
            get
            {
                if (_purchasePlaceRepository == null)
                {
                    _purchasePlaceRepository = new PurchasePlaceRepository(context);
                }

                return _purchasePlaceRepository;
            }
        }

        public IPurchaseRepository PurchaseRepository
        {
            get
            {
                if (_purchaseRepository == null)
                {
                    _purchaseRepository = new PurchaseRepository(context);
                }

                return _purchaseRepository;
            }
        }

        public void Commit()
        {
            context.SaveChanges();
        }
    }
}

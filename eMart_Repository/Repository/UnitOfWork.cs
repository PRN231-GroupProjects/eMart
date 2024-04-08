using eMart_Repository.Entities;
using eMart_Repository.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMart_Repository.Repository
{

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly EMartDbContext? _dbContext;
        private bool disposed = false;
        private IGenericRepository<Category>? _categoryRepository;
        private IGenericRepository<Member>? _memberRepository;
        private IGenericRepository<Order>? _orderRepository;
        private IGenericRepository<OrderDetail>? _orderDetailRepository;
        private IGenericRepository<Product>? _productRepository;
        public UnitOfWork(EMartDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IGenericRepository<Category> CategoryRepository
        {
            get
            {
                return _categoryRepository ??= new GenericRepository<Category>(_dbContext!);
            }
        }
        public IGenericRepository<Member> MemberRepository
        {
            get
            {
                return _memberRepository ??= new GenericRepository<Member>(_dbContext!);
            }
        }
        public IGenericRepository<Order> OrderRepository
        {
            get
            {
                return _orderRepository ??= new GenericRepository<Order>(_dbContext!);
            }
        }
        public IGenericRepository<OrderDetail> OrderDetailRepository
        {
            get
            {
                return _orderDetailRepository ??= new GenericRepository<OrderDetail>(_dbContext!);
            }
        }
        public IGenericRepository<Product> ProductRepository
        {
            get
            {
                return _productRepository ??= new GenericRepository<Product>(_dbContext!);
            }
        }


        public void Save()
        {
            _dbContext!.SaveChanges();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext!.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}

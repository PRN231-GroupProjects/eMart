using eMart_Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMart_Repository.Repository
{

    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Category> CategoryRepository { get; }
        IGenericRepository<Member> MemberRepository { get; }
        IGenericRepository<Order> OrderRepository { get; }
        IGenericRepository<OrderDetail> OrderDetailRepository { get; }
        IGenericRepository<Product> ProductRepository { get; }
        void Save();
    }

}

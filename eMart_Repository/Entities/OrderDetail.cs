using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMart_Repository.Entities
{
    public class OrderDetail:BaseEntity
    {
        public int ProductId { get; set; }  
        public double UnitPrice { get; set; }   
        public int Quantity { get; set; }   
        public int Discount { get; set; }    
        public virtual Product Product { get; set; }    
        public virtual Order Order { get; set; }
    }
}

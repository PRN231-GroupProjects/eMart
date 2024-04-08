using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMart_Repository.Entities
{
    public class Product:BaseEntity
    {
        public string ProductName { get; set; } 
        public double Weight { get; set; }  
        public double UnitPrice { get; set; }   
        public int UnitsInStock { get; set; }   
        public int CategoryId { get; set; } 
        public virtual Category Category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails{ get; set; }
    }
}

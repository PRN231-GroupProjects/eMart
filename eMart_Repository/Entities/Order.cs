using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMart_Repository.Entities
{
    public class Order:BaseEntity
    {        
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public double Freight { get; set; } 
        public int MemberId { get; set; }   
        public virtual Member Member { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }  

    }
}

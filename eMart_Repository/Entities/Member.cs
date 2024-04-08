using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMart_Repository.Entities
{
    public class Member:BaseEntity
    {
        public string Email { get; set; }   
        public string CompanyName { get; set; } 
        public string City { get; set; }    
        public string Country { get; set; }
        public string Password { get; set; }    
        public virtual ICollection<Order> Orders { get; set; }  
    }
}

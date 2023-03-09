using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerPassword { get; set; }
        public bool CustomerStatus { get; set; }

        public DateTime CustomerCreatedTime { get; set; }
        public DateTime CustomerModifiedTime { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
    }
}

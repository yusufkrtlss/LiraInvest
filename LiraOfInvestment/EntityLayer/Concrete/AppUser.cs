
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class AppUser:IdentityUser<int>
    {
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public bool CustomerStatus { get; set; }

        public DateTime CustomerCreatedTime { get; set; }
        public DateTime CustomerModifiedTime { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Favorite
    {

        //public int UserId { get; set; }
        //public int CompanyId { get; set; }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int FavoriteId { get; set; }
        [ForeignKey("Profile")]
        public int ProfileId { get; set; }
        [ForeignKey("AppUser")]
        public int AppUserId { get; set; }
        
        public AppUser AppUser { get; set; }
       
        public Profile Profile { get; set; }
    }
}


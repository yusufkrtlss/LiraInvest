using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string LongName { get; set; }
        public string LongBusinessSummary { get; set; }
        public string Sector { get; set; }
        public string Industry { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
       
        public ICollection<Favorite> Favorites { get; set; }
       // public ICollection<News> News { get; set; }
    }
}

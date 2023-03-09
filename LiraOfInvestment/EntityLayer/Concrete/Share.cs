using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Share
    {
        [Key]
        public int ShareId { get; set; }
        public string ShareName { get; set; }
        public float SharePrice { get; set; }
        public int ShareType { get; set; }
        public string ShareShortName { get; set; }
        public Company Company { get; set; }
    }
}

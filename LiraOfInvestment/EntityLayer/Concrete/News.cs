using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Resource { get; set; }
        public string Link { get; set; }
        public string Type { get; set; }
        public string Which_Symbols { get; set; }
    }
}

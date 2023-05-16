using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class KeyExecutives
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public double? TotalPay { get; set; }
        public double? YearBorn { get; set; }
    }
}

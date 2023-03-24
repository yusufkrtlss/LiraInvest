using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Charts
{
    public class BarChartYearly
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Date { get; set; }
        public long Revenue { get; set; }
        public long Earnings { get; set; }
    }
}

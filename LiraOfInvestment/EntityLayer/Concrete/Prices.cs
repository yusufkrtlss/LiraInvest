using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Prices
    {
        [Key]
        public int Id { get; set; }
        public double MarketChange { get; set; }
        public double MarketChangePercent { get; set; }
        public double DayHigh { get; set; }
        public double DayLow { get; set; }
        public double Open { get; set; }
        public double PreviousClose { get; set; }
        public double Price { get; set; }
        public long Volume { get; set; }
        public string Symbol { get; set; }
        public Profile Profile { get; set; }

    }
}

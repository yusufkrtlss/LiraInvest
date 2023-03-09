using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanySymbol { get; set; }
        public double CompanyRegularMarketPrice { get; set; }
        public double CompanyRegularMarketChange { get; set; }
        public double CompanyRegularMarketChangePercent { get; set; }
        public double CompanyRegularMarketVolume { get; set; }
        public double CompanyRegularMarketDayLow { get; set; }
        public double CompanyRegularMarketDayHigh { get; set; }
        public double CompanyBalance { get; set; }
        public double CompanyIncomeStatement { get; set; }
        public double CompanyEBITDA { get; set; }
        public double CompanyProfit { get; set; }
        public double CompanyPriceGainRate { get; set; }
        public string CompanyInformation { get; set; }
        public ICollection<Share> Shares { get; set; }
        public ICollection<News> News { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
    }
}

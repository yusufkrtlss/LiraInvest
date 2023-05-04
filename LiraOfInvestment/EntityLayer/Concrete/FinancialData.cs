using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class FinancialData
    {
        public int Id { get; set; }
        public double fiftyDayAverage { get; set; }
        public long marketCap { get; set; }
        public long averageVolume10days { get; set; }
        public double twoHundredDayAverage { get; set; }
        public double fiftyTwoWeekHigh { get; set; }
        public double fiftyTwoWeekLow { get; set; }
        public double ebitdaMargins { get; set; }
        public string financialCurrency { get; set; }
        public double grossMargins { get; set; }
        public long grossProfits { get; set; }
        public double revenueGrowth { get; set; }
        public double revenuePerShare { get; set; }
        public double targetHighPrice { get; set; }
        public double targetLowPrice { get; set; }
        public double targetMeanPrice { get; set; }
        public double targetMedianPrice { get; set; }
        public long totalCash { get; set; }
        public double totalCashPerShare { get; set; }
        public long totalDebt { get; set; }
        public long totalRevenue { get; set; }
        public string Symbol { get; set; }
        public Profile Profile { get; set; }

    }
}

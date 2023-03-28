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
        public string Symbol { get; set; }
        public double CurrentPrice { get; set; }
        public double Ratio { get; set; }
        public double DeptToEquity { get; set; }
        public double EarningsGrowth{ get; set; }
        public long Ebitda{ get; set; }
        public long FreeCashFlow{ get; set; }
        public double GrossMargin{ get; set; }
        public string RecommendationKey{ get; set; }
        public double RecommendationMean{ get; set; }
        public double ReturnOnAssets { get; set; }
        public double ReturnOnEquity { get; set; }
        public long TotalCash{ get; set; }
        public double TotalCashPerShare { get; set; }
        public long TotalDept{ get; set; }
        public long TotalRevenue{ get; set; }
        public double RevenueGrowth { get; set; }
        public double RevenuePerShare { get; set; }
        public double ProfitMargin { get; set; }
        public double QuickRatio { get; set; }
    }
}

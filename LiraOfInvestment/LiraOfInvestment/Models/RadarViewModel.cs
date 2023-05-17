using EntityLayer.Concrete;

namespace LiraOfInvestment.Models
{
    public class RadarViewModel
    {
        public string? SymbolFilter { get; set; }
        public string? NameFilter { get; set; }
        public string? IndustryFilter { get; set; }
        public string? SectorFilter { get; set; }
        public double? MinPriceFilter { get; set; }
        public double? MaxPriceFilter { get; set; }
        public double? MinVolumeFilter { get; set; }
        public double? MaxVolumeFilter { get; set; }
        public long? MinTotalCash { get; set; }
        public double? MinTotalCashPerShare { get; set; }
        public long? MinTotalDept { get; set; }
        public long? MinTotalRevenue { get; set; }
        public double? MinRevenueGrowth { get; set; }
        public double? MinRevenuePerShare { get; set; }
        public IQueryable<Profile> profiles { get; set; }
        public IQueryable<Prices> prices { get; set; }
        public IQueryable<FinancialData> financials { get; set; }
    }
}

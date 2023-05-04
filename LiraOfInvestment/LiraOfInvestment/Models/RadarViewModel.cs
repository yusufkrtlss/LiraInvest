using EntityLayer.Concrete;

namespace LiraOfInvestment.Models
{
    public class RadarViewModel
    {
        public string? SymbolFilter { get; set; }
        public string? NameFilter { get; set; }
        public string? IndustryFilter { get; set; }
        public double? MinPriceFilter { get; set; }
        public double? MaxPriceFilter { get; set; }
        public double? MinChangeFilter { get; set; }
        public double? MaxChangeFilter { get; set; }
        public IQueryable<Profile> profiles { get; set; }
        public IQueryable<Prices> prices { get; set; }
    }
}

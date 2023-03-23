using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Profile
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Country { get; set; }
        public string Sector { get; set; }
        public string Industry { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string Currency { get; set; }
        public double DayHigh{ get; set; }
        public double DayLow { get; set; }
        public double _50DayAvg { get; set; }
        public double MarketCap { get; set; }
        public double Open { get; set; }
        public double PreviousClose { get; set; }
        public long Shares{ get; set; }
        public long _10DaysAvgVol{ get; set; }
        public long _3MonthAvgVol{ get; set; }
        public double _200DaysAvg { get; set; }
        public double YearChange { get; set; }
        public double YearHigh { get; set; }
        public double YearLow { get; set; }
    }
}

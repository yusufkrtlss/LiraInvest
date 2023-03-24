using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Charts;
using BusinessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace LiraOfInvestment.ViewComponents.BarChart
{

    public class BarChart:ViewComponent
    {
        IProfileService _profileService;
        IBarChartYearlyService _barChartYearlyService;

        public BarChart(IProfileService profileService, IBarChartYearlyService barChartYearlyService)
        {
            _profileService = profileService;
            _barChartYearlyService = barChartYearlyService;
        }
        public JsonResult Invoke(string id)
        {
            var cid = Convert.ToInt32(id);
            var asset = _profileService.TGetByID(cid);
            var symbol = asset.Symbol;
            var dates = _barChartYearlyService.TGetList().Where(x => x.Symbol.Equals(symbol)).Select(t => t.Date).ToList();
            


            var getRevenue = _barChartYearlyService.TGetList().Where(x => x.Symbol.Equals(symbol)).Select(x => x.Revenue).ToArray();
            var getEarnings = _barChartYearlyService.TGetList().Where(x => x.Symbol.Equals(symbol)).Select(x => x.Earnings).ToArray();


            return new JsonResult(new { timestamp = dates, revenue = getEarnings, earnings=getEarnings });
        }
    }
}

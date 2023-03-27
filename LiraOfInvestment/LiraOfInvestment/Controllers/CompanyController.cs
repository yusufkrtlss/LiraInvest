using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Charts;
using EntityLayer.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;

namespace LiraOfInvestment.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        IProfileService _profileService;
        ITwoYearsMonthly _twoYearsMonthlyService;
        IBarChartYearlyService _barChartYearlyService;
        public CompanyController(IProfileService profileService, ITwoYearsMonthly twoYearsMonthlyService, IBarChartYearlyService barChartYearlyService)
        {
            _profileService = profileService;
            _twoYearsMonthlyService = twoYearsMonthlyService;
            _barChartYearlyService = barChartYearlyService;
        }

        [HttpGet("/company/index/{id}")]
        public IActionResult Index(int id)
        {
            var company=_profileService.TGetByID(id);
            var model = new CompanyProfile()
            {
                Id= company.Id,
                Symbol = company.Symbol,
                Country = company.Country,
                Industry = company.Industry,
                Description = company.Description,
                Website = company.Website,
                Phone = company.Phone,
                DayHigh= company.DayHigh,
                DayLow= company.DayLow,
                _50DayAvg= company._50DayAvg,
                MarketCap= company.MarketCap,
                Open= company.Open,
                PreviousClose= company.PreviousClose,
                Shares= company.Shares,
                _10DaysAvgVol= company._10DaysAvgVol,
                _3MonthAvgVol= company._3MonthAvgVol,
                _200DaysAvg= company._200DaysAvg,
                YearChange= company.YearChange,
                YearHigh= company.YearHigh,
                YearLow= company.YearLow,
            };
            //var chart = GetChartData(id);
            return View(model);
        }
        
        public async Task<JsonResult> GetChartData(string id)
        {
            var cid=Convert.ToInt32(id);
            var asset = _profileService.TGetByID(cid);
            var symbol=asset.Symbol;
            var dates = _twoYearsMonthlyService.TGetList().Where(x => x.Symbol.Equals(symbol)).Select(t => t.Date).ToList();
            var datas = new List<string>();
            foreach (var date in dates)
            {
                var dt=DateTime.Parse(date);
                var data = dt.Date.ToString("MMMM/yy");
                datas.Add(data);
            }

         
            var getClose=_twoYearsMonthlyService.TGetList().Where(x=>x.Symbol.Equals(symbol)).Select(x=>x.Close).ToArray();


            return new JsonResult(new { timestamp = datas, close = getClose });
        }

        public async Task<JsonResult> GetBarChartData(string id)
        {
            var cid = Convert.ToInt32(id);
            var asset = _profileService.TGetByID(cid);
            var symbol = asset.Symbol;
            var dates = _barChartYearlyService.TGetList().Where(x => x.Symbol.Equals(symbol)).Select(t => t.Date).ToList();



            var getRevenue = _barChartYearlyService.TGetList().Where(x => x.Symbol.Equals(symbol)).Select(x => x.Revenue).ToArray();
            var getEarnings = _barChartYearlyService.TGetList().Where(x => x.Symbol.Equals(symbol)).Select(x => x.Earnings).ToArray();


            return new JsonResult(new { timestamp = dates, revenue = getRevenue, earnings = getEarnings });
        }
        [HttpGet]
        public async Task<IActionResult> Compare(int id)
        {
            var companies = _profileService.TGetList();
            var list = companies.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Symbol
            });
            var company = _profileService.TGetByID(id);
            var model = new CompanyProfile()
            {
                Id = company.Id,
                Symbol = company.Symbol,
                Country = company.Country,
                Industry = company.Industry,
                Description = company.Description,
                Website = company.Website,
                Phone = company.Phone,
                DayHigh = company.DayHigh,
                DayLow = company.DayLow,
                _50DayAvg = company._50DayAvg,
                MarketCap = company.MarketCap,
                Open = company.Open,
                PreviousClose = company.PreviousClose,
                Shares = company.Shares,
                _10DaysAvgVol = company._10DaysAvgVol,
                _3MonthAvgVol = company._3MonthAvgVol,
                _200DaysAvg = company._200DaysAvg,
                YearChange = company.YearChange,
                YearHigh = company.YearHigh,
                YearLow = company.YearLow,
                profiles= list,
            };
            //var chart = GetChartData(id);
            return View(model);
        }
        public PartialViewResult CompanyNavbarPartial()
        {        
            return PartialView();
        }
    }
}

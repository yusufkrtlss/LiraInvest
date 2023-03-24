using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Charts;
using EntityLayer.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace LiraOfInvestment.Controllers
{
    [AllowAnonymous]
    public class CompanyController : Controller
    {
        IProfileService _profileService;
        ITwoYearsMonthly _twoYearsMonthlyService;

        public CompanyController(IProfileService profileService, ITwoYearsMonthly twoYearsMonthlyService)
        {
            _profileService = profileService;
            _twoYearsMonthlyService = twoYearsMonthlyService;
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

            //var getClose = (from data in _context.graphDbModel
            //                where data.symbol.Equals("AAPL")
            //                select data.close).ToArray();

            var getClose=_twoYearsMonthlyService.TGetList().Where(x=>x.Symbol.Equals(symbol)).Select(x=>x.Close).ToArray();


            return new JsonResult(new { timestamp = datas, close = getClose });
        }
        public PartialViewResult CompanyNavbarPartial()
        {        
            return PartialView();
        }
    }
}

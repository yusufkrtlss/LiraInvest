using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Charts;
using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using EntityLayer.Dto;
using LiraOfInvestment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Caching.Memory;
using System.Collections;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LiraOfInvestment.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private IProfileService _profileService;
        private ITwoYearsMonthly _twoYearsMonthlyService;
        private IBarChartYearlyService _barChartYearlyService;
        private IFinancialDataService _financialDataService;
        private INewsService _newsService;
        private readonly UserManager<AppUser> _userManager;
        private IUserService _userService;
        private IMemoryCache _cache;
        public CompanyController(IProfileService profileService, ITwoYearsMonthly twoYearsMonthlyService, IBarChartYearlyService barChartYearlyService, IFinancialDataService financialDataService, INewsService newsService, UserManager<AppUser> userManager, IUserService userService, IMemoryCache cache)
        {
            _profileService = profileService;
            _twoYearsMonthlyService = twoYearsMonthlyService;
            _barChartYearlyService = barChartYearlyService;
            _financialDataService = financialDataService;
            _newsService = newsService;
            _userManager = userManager;
            _userService = userService;
            _cache = cache;
        }

        [HttpGet("/company/index/{id}")]
        public async Task<IActionResult> IndexAsync(int id)
        {
           
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var service=_userService.GetAppUserIncludeFavoritesList(user.Id);
            var company=_profileService.TGetByID(id);
            var financialData = _financialDataService.TGetList().Where(x => x.Symbol == company.Symbol).FirstOrDefault();
            var news = _newsService.TGetList().Where(x => x.Which_Symbols.Contains(company.Symbol)).Take(4).ToList();
            var TopFiveCompany=_profileService.TGetList().OrderByDescending(x => x.MarketCap).Take(5).ToList();
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
                FinancialData=financialData,
                News= news,
                AppUser=service,
                TopFiveProfile= TopFiveCompany,
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
            var financialData = _financialDataService.TGetList().Where(x => x.Symbol == company.Symbol).FirstOrDefault();

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
                FinancialData=financialData
            };
            //var chart = GetChartData(id);
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Radar(string symbolFilter,
    string nameFilter,
    string industryFilter,
    double? MinPriceFilter,
    double? MaxPriceFilter,
    double? MinChangeFilter,
    double? MaxChangeFilter)
        {
            var model = new RadarViewModel();
            var query = _profileService.TGetList().AsQueryable();
            model.profiles = query;
            if (!string.IsNullOrEmpty(symbolFilter))
            {
                query = query.Where(s => s.Symbol.Contains(symbolFilter));
                model.profiles = query;
            }

            if (!string.IsNullOrEmpty(nameFilter))
            {
                query = query.Where(s => s.Symbol.Contains(nameFilter));
                model.profiles = query;
            }

            if (!string.IsNullOrEmpty(industryFilter))
            {
                query = query.Where(s => s.Industry.Contains(industryFilter));
                model.profiles = query;
            }

            if (MinPriceFilter.HasValue)
            {
                query = query.Where(s => s.DayLow >= MinPriceFilter.Value);
                model.profiles = query;
            }

            if (MaxPriceFilter.HasValue)
            {
                query = query.Where(s => s.DayLow <= MaxPriceFilter.Value);
                model.profiles = query;
            }

            if (MinChangeFilter.HasValue)
            {
                query = query.Where(s => s.YearChange >= MinChangeFilter.Value);
                model.profiles = query;
            }

            if (MaxChangeFilter.HasValue)
            {
                query = query.Where(s => s.YearChange <= MaxChangeFilter.Value);
                model.profiles = query;
            }
            
            return View(model);
        }
        public PartialViewResult CompanyNavbarPartial()
        {        
            return PartialView();
        }
        public PartialViewResult CompanySidebarPartial(int id)
        {
            
            return PartialView();
        }
        public PartialViewResult CompareCompanies(int id)
        {
            var company = _profileService.TGetByID(id);
            var financialData = _financialDataService.TGetList().Where(x => x.Symbol == company.Symbol).FirstOrDefault();
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
                FinancialData=financialData
            };
            return PartialView("_CompareCompanies",model);
        }
    }
}

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
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
        private IPricesService _priceService;
        private readonly IIncomeStatementService _incomeStatementService;
        private IFavoriteService _favoriteService;
        private ICorporateEventsService _corporateEventsService;
        private IKeyExecutivesService _keyExecutivesService;
        public CompanyController(IProfileService profileService, ITwoYearsMonthly twoYearsMonthlyService, IBarChartYearlyService barChartYearlyService,
            IFinancialDataService financialDataService, INewsService newsService, UserManager<AppUser> userManager, IUserService userService, IPricesService priceService,
            IIncomeStatementService incomeStatementService, IFavoriteService favoriteService, ICorporateEventsService corporateEventsService, IKeyExecutivesService keyExecutivesService)
        {
            _profileService = profileService;
            _twoYearsMonthlyService = twoYearsMonthlyService;
            _barChartYearlyService = barChartYearlyService;
            _financialDataService = financialDataService;
            _newsService = newsService;
            _userManager = userManager;
            _userService = userService;
            _priceService = priceService;
            _incomeStatementService = incomeStatementService;
            _favoriteService = favoriteService;
            _corporateEventsService = corporateEventsService;
            _keyExecutivesService = keyExecutivesService;
        }

        [HttpGet("/company/index/{id}")]
        public async Task<IActionResult> IndexAsync(int id)
        {
           
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var service=_userService.GetAppUserIncludeFavoritesList(user.Id);
            var company=_profileService.TGetByID(id);
            var financialData = _financialDataService.TGetList().Where(x => x.Symbol == company.Symbol).FirstOrDefault();
            var f2 = _favoriteService.GetFavoritesListIncludeProfile(user.Id);
            var prices=_priceService.TGetList().Where(x=>x.Symbol==company.Symbol).First();
            var news = _newsService.TGetList().Where(x => x.Which_Symbols.Contains(company.Symbol)).Take(4).ToList();
            var TopFiveCompany=_profileService.TGetList().Where(x=>x.Sector==company.Sector && x.Symbol!=company.Symbol).Take(4).ToList();
            var incomeStatement=_incomeStatementService.TGetList().Where(x=>x.Symbol==company.Symbol).ToList();
            
            
            var model = new CompanyProfile()
            {
                Profile = company,
                FinancialData=financialData,
                News= news,
                AppUser=service,
                TopFiveProfile= TopFiveCompany,
                prices=prices,
                incomeStatement=incomeStatement,
                favorites=f2,
                
            };
            //var chart = GetChartData(id);
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> CompanyInformation(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var company = _profileService.TGetByID(id);
            var financialData = _financialDataService.TGetList().Where(x => x.Symbol == company.Symbol).FirstOrDefault();
            var prices = _priceService.TGetList().Where(x => x.Symbol == company.Symbol).First();
            var f2 = _favoriteService.GetFavoritesListIncludeProfile(user.Id);
            var corporateEvent=_corporateEventsService.TGetList().Where(x=>x.Symbol == company.Symbol).ToList();
            var executives = _keyExecutivesService.TGetList().Where(x => x.Symbol == company.Symbol).ToList();
            var model = new CompanyProfile()
            {
                Profile = company,
                FinancialData = financialData,
                prices = prices,
                favorites = f2,
                corporateEvents=corporateEvent,
                keyExecutives = executives,
                //FinancialData=financialData
            };
            return View(model);
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
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var company = _profileService.TGetByID(id);
            var financialData = _financialDataService.TGetList().Where(x => x.Symbol == company.Symbol).FirstOrDefault();
            var prices = _priceService.TGetList().Where(x => x.Symbol == company.Symbol).First();
            var f2 = _favoriteService.GetFavoritesListIncludeProfile(user.Id);
            var model = new CompanyProfile()
            {
                Profile = company,
                FinancialData=financialData,
                prices=prices,
                profiles=list,
                favorites= f2
                //FinancialData=financialData
            };
            
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Radar(string symbolFilter,
    string nameFilter,
    string industryFilter,
    string? SectorFilter,
    double? MinPriceFilter,
    double? MaxPriceFilter,
    double? MinVolumeFilter,
    double? MaxVolumeFilter,
    long? MinTotalCash,
    double? MinTotalCashPerShare,
    long? MinTotalDept,
    long? MinTotalRevenue,
    double? MinRevenueGrowth,
    double? MinRevenuePerShare
    )
        {
            List<SelectListItem> sectorList = (from sector in _profileService.TGetList().OrderBy(x => x.Sector).DistinctBy(x=>x.Sector).ToList()
                                               select new SelectListItem()
                                               {
                                                   Text = sector.Sector,
                                                   //Value = sector.Sector.ToString()
                                               }).ToList();
            List<SelectListItem> industryList = (from industry in _profileService.TGetList().OrderBy(x => x.Industry).DistinctBy(x => x.Industry).ToList()
                                               select new SelectListItem()
                                               {
                                                   Text = industry.Industry,
                                                   //Value = sector.Sector.ToString()
                                               }).ToList();
            ViewBag.sectors = sectorList;
            ViewBag.industries = industryList;
            var model = new RadarViewModel();

            var query = _profileService.TGetList().AsQueryable();
            var prices = _priceService.TGetList().AsQueryable();
            var financials = _financialDataService.TGetList().AsQueryable();
            model.prices = prices;
            model.profiles = query;
            if (!string.IsNullOrEmpty(symbolFilter))
            {
                query = query.Where(s => s.Symbol.Contains(symbolFilter.ToUpper()));
                model.profiles = query;
                prices = prices.Where(s => s.Symbol.Contains(symbolFilter));
                model.prices = prices;
            }

            if (!string.IsNullOrEmpty(nameFilter))
            {
                query = query.Where(s => s.Symbol.Contains(nameFilter.ToUpper()));
                model.profiles = query;
                prices = prices.Where(s => s.Symbol.Contains(symbolFilter));
                model.prices = prices;
            }

            if (!string.IsNullOrWhiteSpace(industryFilter))
            {
                query = query.Where(s => s.Industry.Contains(industryFilter));
                model.profiles = query;
                var symbols = query.Select(p => p.Symbol).ToList();
                prices = prices.Where(s => symbols.Contains(s.Symbol));
                model.prices = prices;
            }
            if (!string.IsNullOrEmpty(SectorFilter))
            {
                query = query.Where(s => s.Sector.Contains(SectorFilter));
                model.profiles = query;
                var symbols = query.Select(p => p.Symbol).ToList();
                prices = prices.Where(s => symbols.Contains(s.Symbol));
                model.prices = prices;
            }

            if (MinPriceFilter.HasValue)
            {
                prices = prices.Where(s => s.DayLow >= MinPriceFilter.Value);
                model.prices = prices;
                var symbols = prices.Select(p => p.Symbol).ToList();
                query = query.Where(s => symbols.Contains(s.Symbol));
                model.profiles = query;
            }

            if (MaxPriceFilter.HasValue)
            {
                prices = prices.Where(s => s.DayLow <= MaxPriceFilter.Value);
                model.prices = prices;
                var symbols = prices.Select(p => p.Symbol).ToList();
                query = query.Where(s => symbols.Contains(s.Symbol));
                model.profiles = query;
            }

            if (MinVolumeFilter.HasValue)
            {
                prices = prices.Where(s => s.Volume >= MinVolumeFilter.Value);
                model.prices = prices;
                var symbols = prices.Select(p => p.Symbol).ToList();
                query = query.Where(s => symbols.Contains(s.Symbol));
                model.profiles = query;
            }

            if (MaxVolumeFilter.HasValue)
            {
                prices = prices.Where(s => s.Volume <= MaxVolumeFilter.Value);
                model.prices = prices;
                var symbols = prices.Select(p => p.Symbol).ToList();
                query = query.Where(s => symbols.Contains(s.Symbol));
                model.profiles = query;
            }
            if (MinTotalCash.HasValue)
            {
                financials = financials.Where(s => s.totalCash >= MinTotalCash.Value);
                var symbols = financials.Select(p => p.Symbol).ToList();
                prices = prices.Where(s => symbols.Contains(s.Symbol));
                query = query.Where(s => symbols.Contains(s.Symbol));
                model.profiles = query;
                model.prices = prices;
            }
            if (MinTotalCashPerShare.HasValue)
            {
                financials = financials.Where(s => s.totalCashPerShare >= MinTotalCashPerShare.Value);
                var symbols = financials.Select(p => p.Symbol).ToList();
                prices = prices.Where(s => symbols.Contains(s.Symbol));
                query = query.Where(s => symbols.Contains(s.Symbol));
                model.profiles = query;
                model.prices = prices;
            }
            if (MinTotalDept.HasValue)
            {
                financials = financials.Where(s => s.totalDebt >= MinTotalDept.Value);
                var symbols = financials.Select(p => p.Symbol).ToList();
                prices = prices.Where(s => symbols.Contains(s.Symbol));
                query = query.Where(s => symbols.Contains(s.Symbol));
                model.profiles = query;
                model.prices = prices;
            }
            if (MinTotalRevenue.HasValue)
            {
                financials = financials.Where(s => s.totalRevenue >= MinTotalRevenue.Value);
                model.financials = financials;
                var symbols = financials.Select(p => p.Symbol).ToList();
                prices = prices.Where(s => symbols.Contains(s.Symbol));
                query = query.Where(s => symbols.Contains(s.Symbol));
                model.profiles = query;
                model.prices= prices;
            }
            if (MinRevenueGrowth.HasValue)
            {
                financials = financials.Where(s => s.revenueGrowth >= MinRevenueGrowth.Value);
                var symbols = financials.Select(p => p.Symbol).ToList();
                prices = prices.Where(s => symbols.Contains(s.Symbol));
                query = query.Where(s => symbols.Contains(s.Symbol));
                model.profiles = query;
                model.prices = prices;
            }
            if (MinRevenuePerShare.HasValue)
            {
                financials = financials.Where(s => s.revenuePerShare >= MinRevenueGrowth.Value);
                var symbols = financials.Select(p => p.Symbol).ToList();
                prices = prices.Where(s => symbols.Contains(s.Symbol));
                query = query.Where(s => symbols.Contains(s.Symbol));
                model.profiles = query;
                model.prices = prices;
            }

            return View(model);
        }
        public async Task<JsonResult> GetChartData(string id)
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");
            DateTimeFormatInfo dateTimeFormat = cultureInfo.DateTimeFormat;
            dateTimeFormat.MonthDayPattern = "MMMM d";
            var cid=Convert.ToInt32(id);
            var asset = _profileService.TGetByID(cid);
            var symbol=asset.Symbol;
            var dates = _twoYearsMonthlyService.TGetList().Where(x => x.Symbol.Equals(symbol)).Select(t => t.Date).ToList();
            var datas = new List<string>();
            foreach (var date in dates)
            {
                var dt=DateTime.Parse(date);
                var data = dt.ToString("MMMM/yy",dateTimeFormat);
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
            var trimmedNums= new List<double>();


            var getRevenue = _barChartYearlyService.TGetList().Where(x => x.Symbol.Equals(symbol)).Select(x => x.Revenue).ToArray();
            var getEarnings = _barChartYearlyService.TGetList().Where(x => x.Symbol.Equals(symbol)).Select(x => x.Earnings).ToArray();

           

            return new JsonResult(new { timestamp = dates, revenue = getRevenue, earnings = getEarnings });
        }

        public async Task<JsonResult> GetBubbleChartData(string id)
        {
            var cid = Convert.ToInt32(id);
            var asset = _profileService.TGetByID(cid);
            var symbol = asset.Symbol;
            var dates = _incomeStatementService.TGetList().Where(x => x.Symbol.Equals(symbol)).Select(t => t.Date).ToList();
            var newDates=new List<int>();
            foreach (var date in dates)
            {              
                newDates.Add(date.Year);
            }

            var getRevenue = _incomeStatementService.TGetList().Where(x => x.Symbol.Equals(symbol)).Select(x => x.NetInterestIncome).ToArray();
            //var getEarnings = _barChartYearlyService.TGetList().Where(x => x.Symbol.Equals(symbol)).Select(x => x.Earnings).ToArray();


            return new JsonResult(new { timestamp = newDates, revenue = getRevenue });
        }
        public async Task<JsonResult> GetCostOfRevenueChartData(string id)
        {
            var cid = Convert.ToInt32(id);
            var asset = _profileService.TGetByID(cid);
            var symbol = asset.Symbol;
            var dates = _incomeStatementService.TGetList().Where(x => x.Symbol.Equals(symbol)).Select(t => t.Date).ToList();
            var newDates = new List<int>();
            foreach (var date in dates)
            {
                newDates.Add(date.Year);
            }

            var getRevenue = _incomeStatementService.TGetList().Where(x => x.Symbol.Equals(symbol)).Select(x => x.CostOfRevenue).ToArray();
            //var getEarnings = _barChartYearlyService.TGetList().Where(x => x.Symbol.Equals(symbol)).Select(x => x.Earnings).ToArray();


            return new JsonResult(new { timestamp = newDates, revenue = getRevenue });
        }
        public async Task<JsonResult> GetNetInterestIncomeChartData(string id)
        {
            var cid = Convert.ToInt32(id);
            var asset = _profileService.TGetByID(cid);
            var symbol = asset.Symbol;
            var dates = _incomeStatementService.TGetList().Where(x => x.Symbol.Equals(symbol)).Select(t => t.Date).ToList();
            var newDates = new List<int>();
            foreach (var date in dates)
            {
                newDates.Add(date.Year);
            }

            var getRevenue = _incomeStatementService.TGetList().Where(x => x.Symbol.Equals(symbol)).Select(x => x.EBIT).ToArray();
            //var getEarnings = _barChartYearlyService.TGetList().Where(x => x.Symbol.Equals(symbol)).Select(x => x.Earnings).ToArray();


            return new JsonResult(new { timestamp = newDates, revenue = getRevenue });
        }
        public async Task<JsonResult> GetNetDeptChartData(string id)
        {
            var cid = Convert.ToInt32(id);
            var asset = _profileService.TGetByID(cid);
            var symbol = asset.Symbol;
            var dates = _incomeStatementService.TGetList().Where(x => x.Symbol.Equals(symbol)).Select(t => t.Date).ToList();
            var newDates = new List<int>();
            foreach (var date in dates)
            {
                newDates.Add(date.Year);
            }

            var getRevenue = _incomeStatementService.TGetList().Where(x => x.Symbol.Equals(symbol)).Select(x => x.NetDebt).ToArray();
            //var getEarnings = _barChartYearlyService.TGetList().Where(x => x.Symbol.Equals(symbol)).Select(x => x.Earnings).ToArray();


            return new JsonResult(new { timestamp = newDates, revenue = getRevenue });
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
            var Prices=_priceService.TGetList().Where(x=>x.Symbol==company.Symbol).FirstOrDefault();
            var model = new CompanyProfile()
            {
                Profile=company,
                FinancialData=financialData,
                prices=Prices
            };
            return PartialView("_CompareCompanies",model);
        }
    }
}

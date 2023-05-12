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
        public CompanyController(IProfileService profileService, ITwoYearsMonthly twoYearsMonthlyService, IBarChartYearlyService barChartYearlyService,
            IFinancialDataService financialDataService, INewsService newsService, UserManager<AppUser> userManager, IUserService userService, IPricesService priceService, IIncomeStatementService incomeStatementService, IFavoriteService favoriteService)
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
            var news = _newsService.TGetList().Where(x => x.Which_Symbols.Contains(company.Symbol)).Take(5).ToList();
            var TopFiveCompany=_profileService.TGetList().OrderByDescending(x => x.LongName).Take(5).ToList();
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
                favorites=f2
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
            var model = new CompanyProfile()
            {
                Profile = company,
                FinancialData = financialData,
                prices = prices,
                favorites = f2
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
    double? MinPriceFilter,
    double? MaxPriceFilter,
    double? MinChangeFilter,
    double? MaxChangeFilter)
        {
            var model = new RadarViewModel();
            var query = _profileService.TGetList().AsQueryable();
            var prices = _priceService.TGetList().AsQueryable();
            model.prices = prices;
            model.profiles = query;
            if (!string.IsNullOrEmpty(symbolFilter))
            {
                query = query.Where(s => s.Symbol.Contains(symbolFilter));
                model.profiles = query;
                prices = prices.Where(s => s.Symbol.Contains(symbolFilter));
                model.prices = prices;
            }

            if (!string.IsNullOrEmpty(nameFilter))
            {
                query = query.Where(s => s.Symbol.Contains(nameFilter));
                model.profiles = query;
                prices = prices.Where(s => s.Symbol.Contains(symbolFilter));
                model.prices = prices;
            }

            if (!string.IsNullOrEmpty(industryFilter))
            {
                query = query.Where(s => s.Industry.Contains(industryFilter));
                model.profiles = query;
                prices = prices.Where(s => s.Symbol.Contains(symbolFilter));
                model.prices = prices;
            }

            //if (MinPriceFilter.HasValue)
            //{
            //    query = query.Where(s => s.DayLow >= MinPriceFilter.Value);
            //    model.profiles = query;
            //}

            //if (MaxPriceFilter.HasValue)
            //{
            //    query = query.Where(s => s.DayLow <= MaxPriceFilter.Value);
            //    model.profiles = query;
            //}

            //if (MinChangeFilter.HasValue)
            //{
            //    query = query.Where(s => s.YearChange >= MinChangeFilter.Value);
            //    model.profiles = query;
            //}

            //if (MaxChangeFilter.HasValue)
            //{
            //    query = query.Where(s => s.YearChange <= MaxChangeFilter.Value);
            //    model.profiles = query;
            //}

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
